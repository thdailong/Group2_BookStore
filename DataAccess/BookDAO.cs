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
            Books = context.Books.ToList();                
            return Books;
        }

        public IEnumerable<Book> GetBookListByCate(String category)
        {
            var Books = new List<Book>();
            try
            {
                Books = context.Books.Where(p => p.Category == category).ToList();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Books;
        }
        public Book GetBookById(int BookId)
        {
            Book book = null;
            try
            {
                book = context.Books.SingleOrDefault(c => c.BookId == BookId);
                if(book != null){
                var e = context.Entry(book);
                e.Collection(c => c.OrderDetails).Load();
                e.Reference(p => p.Category).Load();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return book;
        }

        public void AddNew(Book book)
        {
            try
            {
                Book _Book = GetBookById(book.BookId);
                if (_Book == null)
                {
                    context.Books.Add(book);
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

        public void Update(Book book)
        {
            try
            {
                Book _Book = GetBookById(book.BookId);
                if (_Book != null)
                {
                    context.Books.Update(book);
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
                Book book = GetBookById(BookId);
                if (book != null)
                {
                    context.Books.Remove(book);
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