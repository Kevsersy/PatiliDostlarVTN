using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatiliDostlarVTN.Controllers
{
    public class ReferanslarController : Controller
    {
        private readonly HttpClient _client;

        public ReferanslarController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("PatiliDost");
        }

        
        [HttpGet]
        public async Task<IActionResult> Referanslar()
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                var response = await _client.GetAsync("/api/Referanslar");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    comments = JsonSerializer.Deserialize<List<Comment>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    TempData["ErrorMessage"] = "API'den yorumlar alınırken bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Bağlantı sırasında bir hata oluştu: {ex.Message}";
            }

            return View(comments);
        }

      
        [HttpPost]
        public async Task<IActionResult> Referans(string inputname, string inputemail, string inputmessage)
        {
            if (string.IsNullOrWhiteSpace(inputname) || string.IsNullOrWhiteSpace(inputmessage))
            {
                TempData["ErrorMessage"] = "Ad ve mesaj alanları boş olamaz.";
                return RedirectToAction("Referanslar");
            }

            try
            {
                var newComment = new
                {
                    Name = inputname,
                    Message = inputmessage
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(newComment), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("/api/Referanslar", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Yorumunuz başarıyla eklendi!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Yorum eklenirken hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Yorum kaydedilirken bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Referanslar");
        }
    }
}
