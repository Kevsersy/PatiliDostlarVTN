using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;

namespace PatiliDostlarVTN.Controllers;

public class HomeController : Controller
{
    private readonly ICookieService _cookieService;

    public HomeController(ICookieService cookieService)
    {
        _cookieService = cookieService;
    }

    public IActionResult Index()
    {
        var exampleCookie = _cookieService.ReadCookie("ExampleCookie");
        ViewBag.ExampleCookie = exampleCookie;

        return View();
    }

    public IActionResult WriteCookie(string key, string value, int? expireTime)
    {
        _cookieService.WriteCookie(key, value, expireTime);
        TempData["Message"] = "Çerez başarıyla yazıldı!";
        return RedirectToAction("Index");
    }

    public IActionResult ReadCookie(string key)
    {
        var cookieValue = _cookieService.ReadCookie(key);
        TempData["Message"] = cookieValue == null
            ? $"'{key}' isimli bir çerez bulunamadı."
            : $"'{key}' çerezinin değeri: {cookieValue}";

        return RedirectToAction("Index");
    }

    public IActionResult DeleteCookie(string key)
    {
        _cookieService.DeleteCookie(key);
        TempData["Message"] = $"'{key}' çerezi başarıyla silindi.";
        return RedirectToAction("Index");
    }

    public IActionResult UpdateCookie(string key, string newValue, int? expireTime)
    {
        _cookieService.UpdateCookie(key, newValue, expireTime);
        TempData["Message"] = $"'{key}' çerezi başarıyla güncellendi.";
        return RedirectToAction("Index");
    }
}