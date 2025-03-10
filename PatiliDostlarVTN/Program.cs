using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


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



// Repository ve Servis Kayıtları
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICookieService, CookieService>();

// Session ve Cookie Ayarları
builder.Services.AddSession();
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


// Yönlendirme
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}" // Area yönlendirme
);

app.MapDefaultControllerRoute();

app.Run();
