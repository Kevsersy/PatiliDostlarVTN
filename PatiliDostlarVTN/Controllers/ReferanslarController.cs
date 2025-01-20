using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;
using System;

namespace PatiliDostlarVTN.Controllers
{
    public class ReferanslarController : Controller
    {
        private readonly ICommentService _commentService;

        public ReferanslarController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult Referanslar()
        {
     
            var comments = _commentService.GetAllComments();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(comments);
        }

        [HttpPost]
        public IActionResult Referans(string inputname, string inputemail, string inputmessage)
        {
            if (string.IsNullOrWhiteSpace(inputname) || string.IsNullOrWhiteSpace(inputmessage))
            {
                TempData["ErrorMessage"] = "Ad ve mesaj alanları boş olamaz.";
                return RedirectToAction("Referanslar");
            }

            try
            {
                var newComment = new Comment
                {
                    Name = inputname,
                    Message = inputmessage,
                    CreatedAt = DateTime.Now
                };

                _commentService.AddComment(newComment);

                TempData["SuccessMessage"] = "Yorumunuz başarıyla eklendi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Yorum kaydedilirken bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Referanslar");
        }
    }
}
