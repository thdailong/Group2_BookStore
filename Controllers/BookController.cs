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
        private readonly RateDAO rateDAO;
        private readonly CustomerDAO customerDAO;

        public BookController(BOOKSTOREContext db)
        {
            bookDAO = new BookDAO(db);
            commentDAO = new CommentDAO(db);
            rateDAO = new RateDAO(db);
            customerDAO = new CustomerDAO(db);
        }

        public IActionResult Index(int? page)
        {
            if (HttpContext.Session.GetInt32("Status").Value == 2) return RedirectToAction("Index", "Admin");

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

            var customerEmail = HttpContext.Session.GetString("CustomerEmail");
            ViewBag.CusStar = 0;
            if (customerEmail != null)
            {
                ViewBag.CusStar = rateDAO.getRateOnBookAndId(BookId, customerEmail);
            }
            var (x, y) = rateDAO.getRateTotalBaseOnIdBook(BookId);
            ViewBag.starGet = x;
            ViewBag.starTotal = y;
            ViewBag.Star1 = rateDAO.getRateOnIdAndStar(BookId, 1);
            ViewBag.Star2 = rateDAO.getRateOnIdAndStar(BookId, 2);
            ViewBag.Star3 = rateDAO.getRateOnIdAndStar(BookId, 3);
            ViewBag.Star4 = rateDAO.getRateOnIdAndStar(BookId, 4);
            ViewBag.Star5 = rateDAO.getRateOnIdAndStar(BookId, 5);

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

        public Boolean checkIfHaveBuy(string customerEmail, int BookId, List<Order> ls)
        {
            foreach (var item in ls) if (item.Status == 4)
                {
                    var listOrderDetail = item.OrderDetails;
                    var res = listOrderDetail.SingleOrDefault(c => c.BookId == BookId);
                    if (res != null) return true;
                }
            return false;
        }

        public IActionResult AddRateToBook(int BookId, int amountStar)
        {
            var customerEmail = HttpContext.Session.GetString("CustomerEmail");
            if (customerEmail == null)
            {
                TempData["Message"] = "You need to login to rate";
                return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
            }
            if (amountStar < 0 || amountStar > 5)
            {
                TempData["Message"] = "Rate of a book can only from 1 to 5 star";
                return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
            }
            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            if (!checkIfHaveBuy(customerEmail, BookId, cus.Orders))
            {
                TempData["Message"] = "You need to have already buy and received that book to rate";
                return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
            }

            rateDAO.addRate(BookId, customerEmail, amountStar);
            TempData["Success"] = "Update review successfully";

            return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
        }

    }
}