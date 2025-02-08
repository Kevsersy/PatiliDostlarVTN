using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using System.Threading.Tasks;
using System.Linq;
using PatiliDostlarVTN.ViewModels;
using Microsoft.EntityFrameworkCore;


[Authorize(Roles = "Admin")]

[Area("Admin")]
public class HomeController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public HomeController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Users() => View(await _userManager.Users.ToListAsync());
    public async Task<IActionResult> Roles() => View(await _roleManager.Roles.ToListAsync());

    public IActionResult CreatedRole() => View();

    [HttpPost]
    public async Task<IActionResult> CreatedRole(CreateRoleVM model)
    {
        if (ModelState.IsValid)
        {
            var role = new AppRole()
            {
                Name = model.Name,
                CreatedAt = DateTime.Now
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded) return RedirectToAction("Roles");

            result.Errors.ToList().ForEach(f => ModelState.AddModelError(string.Empty, f.Description));
        }
        return View(model);
    }

    public async Task<IActionResult> AssignRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        var userRoles = await _userManager.GetRolesAsync(user);

        var roles = await _roleManager.Roles
                          .Select(s =>
                          new AssingRoleVM
                          (s.Id,
                          s.Name,
                          userRoles.Any(a => a == s.Name)))
                          .ToListAsync();

        return View(new UserRolesVM(user.Id, roles));
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles(UserRolesVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            foreach (var item in model.Roles)
            {
                if (item.IsAssigned)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }

            return RedirectToAction("Users");
        }
        return View(model);
    }



    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction("Users");
        }

        return BadRequest("Kullanıcı silinemedi.");
    }

    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        var model = new EditUserVM
        {
            Id = user.Id,
            Email = user.Email
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserConfirmed(EditUserVM model)
    {
        if (!ModelState.IsValid) return View("EditUser", model);

        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null) return NotFound("Kullanıcı bulunamadı.");

        user.Email = model.Email;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return RedirectToAction("Users");
        }

        ModelState.AddModelError("", "Kullanıcı güncellenemedi.");
        return View("EditUser", model);
    }
}
