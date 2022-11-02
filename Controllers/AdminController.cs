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
    public class AdminController : Controller
    {
        private readonly BookDAO bookDAO;
        private readonly CustomerDAO customerDAO;
        private readonly OrderDAO orderDAO;

        public AdminController(BOOKSTOREContext db) {
            bookDAO = new BookDAO(db);
            customerDAO = new CustomerDAO(db);
            orderDAO = new OrderDAO(db);
        }

        public IActionResult Index() {
            
            return View();
        }
        public IActionResult Book() {
            var booklist = bookDAO.GetBookList();
            return View(booklist);
        }
        public IActionResult Customer() {
            var cuslist = customerDAO.GetCustomerList();
            return View(cuslist);
        }
        public IActionResult Order() {
            var orderlist = orderDAO.GetOrderList();
            return View(orderlist);
        }
    }
}