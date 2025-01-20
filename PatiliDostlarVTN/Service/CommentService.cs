using PatiliDostlarVTN.Models.DTOs;
using PatiliDostlarVTN.Models;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Service
{
    public class CommentService : ICommentService
    {
        private readonly PatiDostumContext _context;

        public CommentService(PatiDostumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return [.. _context.Comments.OrderByDescending(c => c.CreatedAt)];
        }

        public Comment GetCommentByTimeAgo(string timeAgo)
        {
            if (string.IsNullOrWhiteSpace(timeAgo))
                throw new ArgumentException("TimeAgo değeri boş olamaz.", nameof(timeAgo));

            return _context.Comments.FirstOrDefault(c => c.TimeAgo == timeAgo);
        }

        public void CreateComment(CommentCreatDTO model)
        {
            var newComment = new Comment
            {
                AvatarUrl = model.AvatarUrl,
                Message = model.Message,
                TimeAgo = model.TimeAgo,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public void UpdateComment(string timeAgo, CommentUpdate model)
        {
            var existingComment = _context.Comments.FirstOrDefault(c => c.TimeAgo == timeAgo);

            if (existingComment == null)
                throw new KeyNotFoundException("Yorum bulunamadı.");

            existingComment.Message = model.Message;
            existingComment.AvatarUrl = model.AvatarUrl;

            _context.Comments.Update(existingComment);
            _context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
