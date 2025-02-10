using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PatiliDostlarVTN.Validators;

var builder = WebApplication.CreateBuilder(args);

// FluentValidation Ayarları
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = false;
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Veritabanı Bağlantısı
var dbConnectionString = builder.Configuration.GetConnectionString("DbConn");
builder.Services.AddDbContext<PatiDostumContext>(options =>
    options.UseSqlServer(dbConnectionString)
);

//  Identity Konfigürasyonu (Kullanıcı Yönetimi)
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "qwertyuıopğüasdfghjklşizxcvbnmöçQWERTYUIOPĞÜASDFGHJKLŞİZXCVBNMÖÇ0123456789-";
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddUserValidator<CustomUserValidator>()
    .AddPasswordValidator<CustomPassword>()
    .AddEntityFrameworkStores<PatiDostumContext>();

//  Kimlik doğrulama çerez ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/User/Login");
    options.LogoutPath = new PathString("/User/Logout");
    options.AccessDeniedPath = new PathString("/Error/AccessDenied");

    options.Cookie.Name = "CustomIdentityCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

// Repository ve Servis Kayıtları
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICookieService, CookieService>();

// Session ve Cookie Ayarları
builder.Services.AddSession();
builder.Services.AddAuthentication();
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

// Swagger ve API Ayarları
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliştirme Ortamında Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware'ler
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Yönlendirme
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}" // Area yönlendirme
);

app.MapDefaultControllerRoute();

app.Run();
