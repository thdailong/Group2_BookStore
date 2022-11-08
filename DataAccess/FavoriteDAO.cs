using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class FavoriteDAO
    {
        private readonly BOOKSTOREContext context;
        public FavoriteDAO(BOOKSTOREContext _context)
        {
            this.context = _context;
        }

        public Favorite getFavorite(string customer_email, int book_id)
        {
            var res = context.Favorites.SingleOrDefault(c => c.CustomerEmail == customer_email && c.BookId == book_id);
            return res;
        }

        public void addFavorite(string customer_email, int book_id)
        {
            var res = getFavorite(customer_email, book_id);
            if (res == null)
            {
                var entity = new Favorite();
                entity.BookId = book_id;
                entity.CustomerEmail = customer_email;
                context.Favorites.Add(entity);
                context.SaveChanges();
            }
        }

        public void deleteFavorite(string customer_email, int book_id)
        {
            var res = getFavorite(customer_email, book_id);
            context.Favorites.Remove(res);
            context.SaveChanges();
        }

        public IEnumerable<Favorite> getFavoriteOnCustomer(string customer_email)
        {
            var list = context.Favorites.Where(c => c.CustomerEmail == customer_email).ToList();
            if (list.Count() == 0) return list;
            foreach (var item in list) 
            {
                var entry = context.Entry(item);
                entry.Reference(c => c.Book).Load();
            }
            return list;
        }
    }
}