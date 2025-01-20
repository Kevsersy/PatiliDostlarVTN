using PatiliDostlarVTN.Models.DTOs;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Service
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentByTimeAgo(string timeAgo);
        void CreateComment(CommentCreatDTO model);
        void UpdateComment(string timeAgo, CommentUpdate model);
        void AddComment(Comment comment);
    }
}
