using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ContactController : Controller
{
    private readonly PatiDostumContext _context;
    private readonly IContactService _contactService;

    public ContactController(PatiDostumContext context, IContactService contactService)
    {
        _context = context;
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(Contact model)
    {
        if (!ModelState.IsValid)
        {
            return View("Contact", model);
        }

        try
        {
            DateTime today = DateTime.Today;
            DateTime selectedDate = model.AppointmentDate;
            int currentYear = today.Year;

            // 1️⃣ Geçmiş tarihe randevu alınamaz
            if (selectedDate < today)
            {
                TempData["ErrorMessage"] = "Geçmiş tarihe randevu alınamaz.";
                return View("Contact", model);
            }

            // 2️⃣ Mevcut yıl içinde randevu alınabilir, ancak Aralık ayında ise bir sonraki yıl da mümkün
            if (selectedDate.Year != currentYear && !(today.Month == 12 && selectedDate.Year == currentYear + 1))
            {
                TempData["ErrorMessage"] = "Randevu sadece mevcut yıl içinde alınabilir. Aralık ayında ise bir sonraki yıl da mümkündür.";
                return View("Contact", model);
            }

            // 3️⃣ Aynı tarih ve saate randevu kontrolü
            bool isTimeSlotTaken = _context.Contacts
                .Any(r => r.AppointmentDate == model.AppointmentDate && r.AppointmentTime == model.AppointmentTime);

            if (isTimeSlotTaken)
            {
                TempData["ErrorMessage"] = "Bu tarih ve saat için zaten bir randevu mevcut. Lütfen başka bir zaman seçin.";
                return View("Contact", model);
            }

            // 4️⃣ Veriyi veritabanına ekleme
            _context.Add(model);
            await _context.SaveChangesAsync();

            // Başarı mesajını TempData ile kullanıcıya göster
            TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";

            // Kullanıcıyı başarı sayfasına yönlendir
            return RedirectToAction("Randevu");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Randevu oluşturulurken bir hata oluştu.";
            Console.WriteLine("Hata: " + ex.Message);
            return View("Contact", model);
        }
    }

    public IActionResult Randevu()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetUnavailableTimes(DateTime date)
    {
        var unavailableTimes = _contactService.GetUnavailableTimes(date);
        return Json(unavailableTimes);
    }
}
