using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PatiliDostlarVTN.Areas.Admin.Controllers;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.DTOs;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Service;
using System.Collections.Generic;
using System.Linq;

namespace PatiliDostlarVTN.Controllers;

[Route("api/v1/comments")]
[ApiController]
public class ApıController : ControllerBase
{
    private readonly ICommentService _commentService;

    public ApıController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommentDTO>> Get()
    {
        var comments = _commentService.GetAllComments();
        return Ok(comments);
    }

    [HttpGet("{timeAgo}")]
    public ActionResult<CommentDTO> Get(string timeAgo)
    {
        var comment = _commentService.GetCommentByTimeAgo(timeAgo);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    [HttpPost]
    public ActionResult Post(CommentCreatDTO model)
    {
        if (!ModelState.IsValid) return BadRequest();

        _commentService.CreateComment(model);
        return CreatedAtAction(nameof(Get), new { timeAgo = model.TimeAgo }, model);
    }

    [HttpPut("{timeAgo}")]
    public IActionResult Put(string timeAgo, CommentUpdate model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Model geçerli değil.");

        try
        {
            _commentService.UpdateComment(timeAgo, model); 
            return NoContent(); 
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Güncellenecek yorum bulunamadı."); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }

}
