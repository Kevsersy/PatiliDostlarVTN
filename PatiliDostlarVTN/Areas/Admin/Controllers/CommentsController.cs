using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PatiliDostlarVTN.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly HttpClient _client;

        public CommentsController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("PatiliDost");
        }

        
        public async Task<IActionResult> Index()
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

       
        public async Task<IActionResult> Details(int id)
        {
            var comment = await GetCommentFromApi(id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (!ModelState.IsValid)
                return View(comment);

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/api/Referanslar", jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Yorum eklenirken hata oluştu.";
            }

            return View(comment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comment = await GetCommentFromApi(id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment comment)
        {
            if (id != comment.ID || !ModelState.IsValid)
                return BadRequest();

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"/api/Referanslar/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Yorum güncellenirken hata oluştu.";
            }

            return View(comment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comment = await GetCommentFromApi(id);
            if (comment == null)
                return NotFound();

            return View(comment);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"/api/Referanslar/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Yorum silinirken hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }

        
        private async Task<Comment> GetCommentFromApi(int id)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Referanslar/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Comment>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Yorum verisi alınırken hata oluştu.";
            }

            return null;
        }
    }
}
