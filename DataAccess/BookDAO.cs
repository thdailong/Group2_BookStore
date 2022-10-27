using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookDAO
    {
       private readonly BOOKSTOREContext context; 
        public BookDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }
        public IEnumerable<Book> GetBookList()
        {
            var Books = new List<Book>();
            try
            {
                Books = context.Books.ToList();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Books;
        }

        public IEnumerable<Book> GetBookListByCate(String category)
        {
            var Books = new List<Book>();
            try
            {
                Books = context.Books.ToList();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Books;
        }
        public Book GetBookById(int BookId)
        {
            Book Book = null;
            try
            {
                Book = context.Books.SingleOrDefault(c => c.BookId == BookId);
                if(Book != null){
                var e = context.Entry(Book);
                e.Collection(c => c.OrderDetails).Load();
                e.Reference(p => p.Category).Load();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Book;
        }

        public void AddNew(Book book)
        {
            try
            {
                Book _Book = GetBookById(Book.BookId);
                if (_Book == null)
                {
                    context.Books.Add(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Book Book)
        {
            try
            {
                Book _Book = GetBookById(Book.BookId);
                if (_Book != null)
                {
                    context.Books.Update(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int BookId)
        {
            try
            {
                Book Book = GetBookById(BookId);
                if (Book != null)
                {
                    context.Books.Remove(Book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Book does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}