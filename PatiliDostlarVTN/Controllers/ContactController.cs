using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Submit(Contact contact)
    {
        if (!_contactService.ValidateContact(contact, out var errorMessage))
        {
            TempData["ErrorMessage"] = errorMessage;
            return View("Contact", contact);
        }

        _contactService.AddContact(contact);

        TempData["SuccessMessage"] = "Randevunuz başarılı bir şekilde oluşturuldu!";
        return RedirectToAction("Contact");
    }

    [HttpGet]
    public IActionResult GetUnavailableTimes(DateTime date)
    {
        var unavailableTimes = _contactService.GetUnavailableTimes(date);
        return Json(unavailableTimes);
    }
}
