using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DataAccess;
using Group2_BookStore.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartDAO cartDAO;

        public CartController(BOOKSTOREContext db) {
            cartDAO = new CartDAO(db);
        }

        public IActionResult Index()
        {
            var mylist = cartDAO.GetCartList();
            ViewBag.Books = mylist;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}