using DiabetProject.BLL.Data;
using DiabetProject.BLL.Data.Entities;
using DiabetProject.BLL.Data.Hubs;
using DiabetProject.BLL.Repo;
using DiabetProject.BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//cookiee
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(props =>
{
    props.Cookie.Name = "GeneMap.Auth";
    props.LoginPath = "/Auth/Login";
    props.AccessDeniedPath = "/Auth/Login";
    props.LogoutPath = "/Auth/Logout";
});
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<DiabetDiagnosisRepo>();

builder.Services.AddDbContext<DiabetsDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    // options.SignIn.RequireConfirmedEmail = true; //email onaylý mý deðil mi bakar
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
}).AddEntityFrameworkStores<DiabetsDataContext>().AddDefaultTokenProviders();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();




var app = builder.Build();
app.MapHub<DiagnosisHub>("/diagnosisHub");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var srv = scope.ServiceProvider;
    var context = srv.GetRequiredService<DiabetsDataContext>();
    context.Database.Migrate();
}
app.Run();



