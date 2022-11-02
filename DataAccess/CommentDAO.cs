using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class CommentDAO
    {
        private readonly BOOKSTOREContext context;
        public CommentDAO(BOOKSTOREContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// Get Comment base on the Id of comment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Comment model</returns>
        public Comment GetCommentOnId(int Id)
        {
            var res = this.context.Comments.Find(Id);
            return res;
        }

        /// <summary>
        /// Add Comment from parameter to database
        /// </summary>
        /// <param name="comment">Comment model</param>
        public void AddComment(Comment comment)
        {
            var list = context.Comments.ToList();
            comment.CommendId = list.Max(c => c.CommendId) + 1;
            this.context.Comments.Add(comment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Return list comments base on customer email
        /// </summary>
        /// <param name="CustomerEmail"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetListCommentOnEmail(string CustomerEmail)
        {
            var res = context.Comments.Where(c => c.CustomerEmail == CustomerEmail).ToList();
            return res;
        }

        /// <summary>
        /// Get comments of a book base on id
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetListCommentOnBookId(int BookId)
        {
            var res = context.Comments.Where(c => c.BookId == BookId).ToList();
            foreach (var item in res)
            {
                var entry = context.Entry(item);
                entry.Reference(c => c.CustomerEmailNavigation).Load();
            }
            return res;
        }

        /// <summary>
        /// Update Comment from parameter to database
        /// </summary>
        /// <param name="comment">Comment model</param>
        public void UpdateComment(Comment comment)
        {
            this.context.Comments.Update(comment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete comment base on Id given
        /// </summary>
        /// <param name="Id">Comment Id</param>
        public void DeleteComment(int Id)
        {
            var res = GetCommentOnId(Id);
            this.context.Comments.Remove(res);
        }

        /// <summary>
        /// Delete a comment with Id given but only from the user that make this comment
        /// </summary>
        /// <param name="CustomerEmail">The email of the user</param>
        /// <param name="Id">Id of the comment</param>
        /// <returns>True for delete successful, False for other wise</returns>
        public Boolean DeleteCommentFromOwnUser(string CustomerEmail, int Id)
        {
            var res = GetCommentOnId(Id);
            if (res.CustomerEmail == CustomerEmail)
            {
                DeleteComment(Id);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}