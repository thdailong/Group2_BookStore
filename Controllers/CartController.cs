using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly BOOKSTOREContext _db;

        public CartController(BOOKSTOREContext db) {
            this._db = db;
        }

        public IActionResult Index()
        {
            var mylist = this._db.Books.ToList();
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