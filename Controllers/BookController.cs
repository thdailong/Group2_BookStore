using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Group2_BookStore.DataAccess;
using Group2_BookStore.DB;
using Group2_BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDAO bookDAO;
        private readonly CommentDAO commentDAO;

        public BookController(BOOKSTOREContext db)
        {
            bookDAO = new BookDAO(db);
            commentDAO = new CommentDAO(db);
        }

        public IActionResult Index(int? page)
        {
            if (page == null) page = 1;
            if (page.Value < 1) return NotFound();
            var listBook = bookDAO.GetBooksListOnPage(page.Value - 1);
            if (listBook == null) return NotFound();
            ViewBag.listBook = listBook;
            ViewBag.pageNumber = page.Value;
            var tmp = bookDAO.GetBookList();
            ViewBag.totalPage = (tmp.Count() + 11) / 12;
            return View();
        }

        public IActionResult SearchOnCat(int? page, string cat_name)
        {
            if (page == null) page = 1;
            if (page.Value < 1) return NotFound();
            var listBook = bookDAO.GetBooksListOnCatWithPage(page.Value - 1, cat_name);
            if (listBook == null) return NotFound();
            ViewBag.listBook = listBook;
            ViewBag.pageNumber = page.Value;
            var tmp = bookDAO.GetBookListByCate(cat_name);
            ViewBag.totalPage = (tmp.Count() + 11) / 12;
            ViewBag.cat_name = cat_name;
            ViewBag.totalBooks = tmp.Count();
            return View();
        }

        public IActionResult Search(int? page, string name)
        {
            if (page == null) page = 1;
            if (page.Value < 1) return NotFound();
            var listBook = bookDAO.GetBooksSearchOnPage(page.Value - 1, name);
            if (listBook == null) return NotFound();
            ViewBag.listBook = listBook;
            ViewBag.pageNumber = page.Value;
            var tmp = bookDAO.GetBooksSearch(name);
            ViewBag.totalPage = (tmp.Count() + 11) / 12;
            ViewBag.name = name;
            ViewBag.totalBooks = tmp.Count();
            return View();
        }

        public IActionResult BookDetail(int BookId)
        {
            var book = bookDAO.GetBookById(BookId);
            var listCom = (List<Comment>)commentDAO.GetListCommentOnBookId(BookId);
            ViewBag.listCom = listCom;
            ViewBag.commentCount = listCom.Count();
            return View(book);
        }

        public IActionResult AddComment(string ContentComment, int BookId)
        {
            var customerEmail = HttpContext.Session.GetString("CustomerEmail");
            if (customerEmail == null)
            {
                TempData["Message"] = "You need to login so as comment";
                return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
            }
            var comment = new Comment();
            comment.BookId = BookId;
            comment.ContentComment = ContentComment;
            comment.TimeComment = DateTime.Now;
            comment.CustomerEmail = customerEmail;
            commentDAO.AddComment(comment);
            TempData["Success"] = "Add comment successfully";

            return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
        }


    }
}