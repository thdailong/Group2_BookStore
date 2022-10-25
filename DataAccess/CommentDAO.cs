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
        public CommentDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// Get Comment base on the Id of comment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Comment model</returns>
        public Comment GetCommentOnId(int Id) {
            var res = this.context.Comments.Find(Id);
            return res;
        }
        
        /// <summary>
        /// Add Comment from parameter to database
        /// </summary>
        /// <param name="comment">Comment model</param>
        public void AddComment(Comment comment) {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Update Comment from parameter to database
        /// </summary>
        /// <param name="comment">Comment model</param>
        public void UpdateComment(Comment comment) {
            this.context.Comments.Update(comment);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete comment base on Id given
        /// </summary>
        /// <param name="Id">Comment Id</param>
        public void DeleteComment(int Id) {
            var res = GetCommentOnId(Id);
            this.context.Comments.Remove(res);
        }

        /// <summary>
        /// Delete a comment with Id given but only from the user that make this comment
        /// </summary>
        /// <param name="CustomerEmail">The email of the user</param>
        /// <param name="Id">Id of the comment</param>
        /// <returns>True for delete successful, False for other wise</returns>
        public Boolean DeleteCommentFromOwnUser(string CustomerEmail, int Id) {
            var res = GetCommentOnId(Id);
            if (res.CustomerEmail == CustomerEmail) {
                DeleteComment(Id);
                return true;
            }
            else {
                return false;
            }
        }
    }
}