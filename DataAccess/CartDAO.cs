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

        /// <summary>
        /// Add a book to the cart if cart have not any book
        /// In case that book already in the cart, increase the quantity of that book
        /// </summary>
        /// <param name="BookId">Id of the book</param>
        /// <param name="Quantity">Quantity of the book</param>
        /// <param name="CustomerEmail">Email of the user</param>
        public void AddBookToCart(int BookId, int Quantity, string CustomerEmail) {
            var bookInCart = new Cart();
            bookInCart.BookId = BookId;
            bookInCart.CustomerEmail = CustomerEmail;
            bookInCart.Quantity = Quantity;
            var finding = this.context.Carts.Where(c => c.CustomerEmail == CustomerEmail && c.BookId == BookId).SingleOrDefault();
            if (finding == null) {
                this.context.Carts.Add(bookInCart);
            }
            else {
                finding.Quantity+=Quantity;
            }
            this.context.SaveChanges();
        }
    }
}