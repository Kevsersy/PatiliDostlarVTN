using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.DTOs;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Controllers
{
    [Route("api/v1/Comments")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly PatiDostumContext _db;

        public APIController(PatiDostumContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public IEnumerable<CommentDTO> Get()
        {
            List<CommentDTO> response = [];

            foreach (var item in _db.Comments)
            {


                response.Add(new CommentDTO(item.Name, item.Message,item.ID));
            }
            return response;
        }

       [HttpGet("{Name}")]
public ActionResult<CommentDTO> Get(string Name)
{
    var comment = _db.Comments.FirstOrDefault(c => c.Name == Name);
    if (comment is null) return NotFound();

    return Ok(new CommentDTO(comment.Name, comment.Message,comment.ID));
}

[HttpPost]
public ActionResult Post([FromBody] CommentCreatDTO model)
{
    if (!ModelState.IsValid)
    {
        return BadRequest();
    }

    var comment = new Comment { Message = model.Message, Name = model.Name };

    _db.Comments.Add(comment);
    _db.SaveChanges();

    return CreatedAtAction(nameof(Get), new { Name = comment.Name }, new CommentDTO(comment.Name, comment.Message,comment.ID));
}


        [HttpPut("{id}")]

        public ActionResult Put(int id, CommentUpdate model)
        {

            var comment = _db.Comments.Find(id);

            if( comment is null)
            {
                comment=new Comment
                {

                    Name=model.Name,
                    Message=model.message,
                };
                _db.Comments.Add(comment);
                _db.SaveChanges();


                return CreatedAtAction("Get", new { id = comment.ID }, new CommentDTO( comment.Name, comment.Message,comment.ID));
            }
            comment.Name = model.Name;
            comment.Message = model.message;
            _db.SaveChanges();

            return NoContent();
        }

           
    }
}