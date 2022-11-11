using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Group2_BookStore.DataAccess;
using Group2_BookStore.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDAO bookDAO;
        private readonly CommentDAO commentDAO;
        private readonly RateDAO rateDAO;
        private readonly CustomerDAO customerDAO;

        public HomeController(BOOKSTOREContext db)
        {
            bookDAO = new BookDAO(db);
            commentDAO = new CommentDAO(db);
            rateDAO = new RateDAO(db);
            customerDAO = new CustomerDAO(db);
        }

        public IActionResult Index()
        {
            var listBook = bookDAO.GetListBookHome();
            listBook.Reverse();
            ViewBag.listBook = listBook;
            
            var newLx = bookDAO.GetListBookHome();
            ViewBag.listTopRated = newLx;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }   
    }
}
