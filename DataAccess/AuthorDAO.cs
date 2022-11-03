using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class AuthorDAO
    {
        private readonly BOOKSTOREContext context; 
        public AuthorDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// Get Author base on the Id of Author
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Author model</returns>
        public Author GetAuthorOnId(int Id) {
            var res = this.context.Authors.Find(Id);
            return res;
        }
        public IEnumerable<Author> GetAuthorList() {
            return this.context.Authors.ToList();
        }
        
        /// <summary>
        /// Add Author from parameter to database
        /// </summary>
        /// <param name="Author">Author model</param>
        public void AddAuthor(Author Author) {
            this.context.Authors.Add(Author);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Update Author from parameter to database
        /// </summary>
        /// <param name="Author">Author model</param>
        public void UpdateAuthor(Author Author) {
            this.context.Authors.Update(Author);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete Author base on Id given
        /// </summary>
        /// <param name="Id">Author Id</param>
        public void DeleteAuthor(int Id) {
            var res = GetAuthorOnId(Id);
            this.context.Authors.Remove(res);
        }

    }
}