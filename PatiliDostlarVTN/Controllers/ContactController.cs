using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.ViewModels;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ContactController : Controller
{
    private readonly HttpClient _client;

    public ContactController(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("PatiliDost");
    }

    
    [HttpGet]
    public IActionResult Contact()
    {
        var viewModel = new ContactVM
        {
            NewContact = new Contact() 
        };

        return View(viewModel);
    }

   
    [HttpPost]
    public async Task<IActionResult> Submit(ContactVM model)
    {
        if (!ModelState.IsValid)
        {
            return View("Contact", model);
        }

        try
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(model.NewContact), System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Contacts", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Randevu oluşturulamadı. Aynı tarih ve saate randevu var mı kontrol edin.";
                return View("Contact", model);
            }

            TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu!";

           
            return RedirectToAction("Randevu");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Randevu oluşturulurken bir hata oluştu.";
            Console.WriteLine("Hata: " + ex.Message);
            return View("Contact", model);
        }
    }

    
    [HttpGet]
    public IActionResult Randevu()
    {
        return View();
    }
}
