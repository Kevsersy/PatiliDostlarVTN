using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class HomeController : Controller
{
    private const string AdminEmail = "akinkevser48@gmail.com";
    private const string AdminPassword = "123";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Logins(string email, string password)
    {
        if (email == AdminEmail && password == AdminPassword)
        {
            // Giriş başarılı, AboutUs sayfasına yönlendirme yapılır
            return RedirectToAction("Index", "AboutUs");
        }

        // Giriş başarısız, hata mesajı ile tekrar giriş sayfasına dön
        ViewBag.ErrorMessage = "Hatalı email veya şifre!";
        return View("Index");
    }

    public IActionResult Login()
    {
        return View();
    }
}
