using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatiliDostlarVTN.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(SignupVM model)
        {
            if (ModelState.IsValid)
            {
              
                var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");

                if (!passwordRegex.IsMatch(model.Password))
                {
                    ModelState.AddModelError("Password", "Şifre en az 6 karakter olmalı, bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
                    return View(model);
                }

              
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
                    return View(model);
                }

                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                var existingUsername = await _userManager.FindByNameAsync(model.UserName);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanımda.");
                    return View(model);
                }

                if (existingUsername != null)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanımda.");
                    return View(model);
                }

                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    BoD = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                
                    await _userManager.AddToRoleAsync(user, "User");

                    return RedirectToAction("Success");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Giris(string? returnUrl)
        {
            if (returnUrl is not null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Giris(Giris model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                    return View(model);
                }

            
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    ModelState.AddModelError("", "Yetkisiz giriş! Sadece admin kullanıcıları giriş yapabilir.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("AboutUs", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz giriş bilgileri.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
