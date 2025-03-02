using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly HttpClient _client;

        public ContactsController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("PatiliDost");
        }

     
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                var response = await _client.GetAsync("/api/Contacts");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    contacts = JsonSerializer.Deserialize<List<Contact>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "API'den veriler alınırken hata oluştu.";
            }

            return View(contacts);
        }

     
        public async Task<IActionResult> Details(int id)
        {
            var contact = await GetContactFromApi(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }

    
        public IActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/api/Contacts", jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Randevu oluşturulurken hata oluştu.";
            }

            return View(contact);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await GetContactFromApi(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ID || !ModelState.IsValid)
                return BadRequest();

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"/api/Contacts/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Randevu güncellenirken hata oluştu.";
            }

            return View(contact);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await GetContactFromApi(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"/api/Contacts/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Randevu silinirken hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<Contact> GetContactFromApi(int id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Contacts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Contact>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Randevu verisi alınırken hata oluştu.";
            }

            return null;
        }
    }
}
