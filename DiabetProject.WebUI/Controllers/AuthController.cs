using DiabetProject.BLL.Data.Dto;
using DiabetProject.BLL.Data.Entities;
using DiabetProject.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DiabetProject.WebUI.Controllers
{
    public class AuthController(UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,EmailService emailService)  : Controller
    {
        
        private readonly EmailService _emailService=emailService;
      


        //dependcyye bakılcak
        //dependcyye bakılcak

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register, CancellationToken cancellationToken)
        {
            AppUser user = new()
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName
            };
            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                return Redirect("/Auth/Login");
            }

            return BadRequest(result.Errors.Select(x => x.Description));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            AppUser? appuser = await userManager.FindByIdAsync(changePasswordDto.Id.ToString());
            if (appuser == null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }

            IdentityResult result = await userManager.ChangePasswordAsync(appuser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));

            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }

            string token = await userManager.GeneratePasswordResetTokenAsync(appUser);
            string resetLink = Url.Action("ResetPassword", "Auth",new {token,email},Request.Scheme);

            string emailBody = $"<h3>Şifre Sıfırlama</h3><p>Şifrenizi sıfırlamak için <a href='{resetLink}'>buraya tıklayın</a>.</p>";
            await _emailService.SendEmail(email, "Şifre Sıfırlama", emailBody);
            ViewBag.Message = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.";
            return View();
            //return RedirectToAction("ChangePasswordUsingToken", new { Token = token });
        }


        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Geçersiz istek.");
            }
            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ChangePasswordUsingTokenDto passwordDto)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(passwordDto.Email);
            if (appUser == null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }
            IdentityResult result = await userManager.ResetPasswordAsync(appUser, passwordDto.Token, passwordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));

            }
            //bool isTokenValid = true; // Burada gerçek kontrolü yapmalısın

            //if (!isTokenValid)
            //{
            //    return BadRequest("Geçersiz veya süresi dolmuş bağlantı.");
            //}

            // Kullanıcının şifresini güncelle (Burada DB işlemi yapmalısın)
            

            if (passwordDto.NewPassword !=null)
            {
                ViewBag.Message = "Şifreniz başarıyla sıfırlandı.";
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ModelState.AddModelError("", "Şifre sıfırlama işlemi başarısız.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordUsingToken(ChangePasswordUsingTokenDto passwordDto, CancellationToken cancellationToken)
        {

          
            return NoContent();
        }


        [HttpGet]
        //[ValidateAntiForgeryToken] // CSRF koruması için
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.UserNameOrEmail || x.UserName == loginDto.UserNameOrEmail, cancellationToken);
            if (appUser == null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
            }

            SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Şifre Yanlış!");
                return RedirectToAction(nameof(Login), signInResult);
            }
            // Kullanıcıyı oturum açmış hale getir
            await signInManager.SignInAsync(appUser, isPersistent: false);

            return Redirect("/Home");
        }
    }
}
