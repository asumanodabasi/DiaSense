using DiabetProject.BLL.Data.Dto;
using DiabetProject.BLL.Data.Entities;
using DiabetProject.BLL.Data.Hubs;
using DiabetProject.BLL.Extensions;
using DiabetProject.BLL.Repo;
using DiabetProject.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DiabetProject.BLL.Data.Dto.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DiabetProject.WebUI.Controllers
{
    [Authorize]
    public class DiabetsController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly DiabetDiagnosisRepo _diabetDiagnosisRepo;
        private readonly IHubContext<DiagnosisHub> _hubContext;
        public DiabetsController(HttpClient httpClient, DiabetDiagnosisRepo diabetDiagnosisRepo, IHubContext<DiagnosisHub> hubContext, UserManager<AppUser> userManager) :base(userManager)
        {
            _httpClient = httpClient;
            _diabetDiagnosisRepo = diabetDiagnosisRepo;
            _hubContext = hubContext;
            // _httpClient.BaseAddress = new Uri("http://localhost:5000"); //Api for python
        }

        [HttpGet]
        public async Task<IActionResult> Predict()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Predict(DiagnosisDto request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error");
            }
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = null // if camelCase'e çevirmesin 
            };
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurun");
                return View();
            }
            var json = JsonSerializer.Serialize(request, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("http://localhost:5000/predict", content);

            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PredictionResponse>();
                var prediction = result?.Prediction;
                var pre = result?.Prediction == 1 ? "Diyabet Var" : "Diyabet Yok";
                ViewBag.Pre = result?.Prediction;
                return Json(new { prediction=prediction , pre=pre});
            }
            else
            {
                ViewBag.Prediction = $"API çağrısı başarısız!  Hata kodu: {response.StatusCode}";
                return Json(new { prediction = $"API başarısız! Hata Kodu: {response.StatusCode}" });
            }
            
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(DiagnosisDto diagnosisDto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdString);
            diagnosisDto.UserId = userId;

            diagnosisDto.Bmi = diagnosisDto.Weight / diagnosisDto.Height;
            var result = await _diabetDiagnosisRepo.Add(diagnosisDto);

            await _hubContext.Clients.All.SendAsync("ReceiveChartUpdate", new List<DiagnosisDto> { result });
            return Redirect("/GetAllByUser");
        }

        [HttpGet("GetAllByUser")]
        public async Task<IActionResult> GetAllByUser()
        {

            var userIdString =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdString);
            var result = await _diabetDiagnosisRepo.GetAll(userId);

            await _hubContext.Clients.All.SendAsync("ReceiveChartUpdate",result);

            return View(result);
        }

    }
}
