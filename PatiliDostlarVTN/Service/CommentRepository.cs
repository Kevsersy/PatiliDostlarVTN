using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;
using PatiliDostlarVTN.Models;

namespace PatiliDostlarVTN.Service
{
  
    public class CommentRepository : ICommentRepository
    {
        private readonly PatiDostumContext _db;

        public CommentRepository(PatiDostumContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// Veritabanındaki tüm yorumları alır.
        /// </summary>
        /// <returns>Yorumların listesi</returns>
        public IEnumerable<Comment> GetAllComments()
        {
            return _db.Comments
                     .AsNoTracking() 
                     .OrderByDescending(c => c.CreatedAt) 
                     .ToList();
        }

        /// <summary>
        /// Belirtilen 'timeAgo' değerine sahip bir yorumu getirir.
        /// </summary>
        /// <param name="timeAgo">Aranan zaman bilgisi</param>
        /// <returns>Yorum veya null</returns>
        public Comment GetCommentByTimeAgo(string timeAgo)
        {
            if (string.IsNullOrWhiteSpace(timeAgo))
                throw new ArgumentException("TimeAgo değeri boş olamaz.", nameof(timeAgo));

            return _db.Comments
                     .AsNoTracking()
                     .FirstOrDefault(c => c.TimeAgo == timeAgo);
        }

        /// <summary>
        /// Yeni bir yorumu veritabanına ekler.
        /// </summary>
        /// <param name="comment">Eklenecek yorum</param>
        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        /// <summary>
        /// Var olan bir yorumu günceller.
        /// </summary>
        /// <param name="comment">Güncellenecek yorum</param>
        public void UpdateComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            var existingComment = _db.Comments.Find(comment.ID);
            if (existingComment == null)
                throw new KeyNotFoundException("Güncellenecek yorum bulunamadı.");

            existingComment.Message = comment.Message;
            existingComment.AvatarUrl = comment.AvatarUrl;
            existingComment.TimeAgo = comment.TimeAgo;

            _db.Comments.Update(existingComment);
            _db.SaveChanges();
        }
    }
}
