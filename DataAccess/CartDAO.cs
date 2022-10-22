using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class CartDAO
    {
        private readonly BOOKSTOREContext context; 
        public CartDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// This one is for testing purpose only
        /// </summary>
        /// <returns>List of books</returns>
        public IEnumerable<Book> GetCartList() {
            var lis = context.Books.ToList();
            return lis;
        }
    }
}