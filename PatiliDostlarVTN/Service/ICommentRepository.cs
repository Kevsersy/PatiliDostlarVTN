using PatiliDostlarVTN.Models.Entities;
using System.Collections.Generic;

namespace PatiliDostlarVTN.Service
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentByTimeAgo(string timeAgo);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
    }
}
