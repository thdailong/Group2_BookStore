using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Group2_BookStore.DataAccess;
using Group2_BookStore.DB;
using Group2_BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookDAO bookDAO;
        private readonly CustomerDAO customerDAO;
        private readonly OrderDAO orderDAO;
        private readonly DetailOrderDAO detailDAO;
        private readonly AuthorDAO authorDAO;
        

        public AdminController(BOOKSTOREContext db) {
            bookDAO = new BookDAO(db);
            customerDAO = new CustomerDAO(db);
            orderDAO = new OrderDAO(db);
            detailDAO = new DetailOrderDAO(db);
            authorDAO = new AuthorDAO(db);
        }
        
        public IActionResult Index() {
            var order_detail_done = detailDAO.GetListOrderDetailOnStatus(4);
            var array = new List<string>() {
                "Motivation",
                "Fantasy",
                "Sci-Fi",
                "Mystery",
                "Manga",
                "Romance",
                "Travel",
                "Guide/How to",
                "Self-help",
                "Textbook",
                "Comic"
            };
            int[] quantity_cate = new int[11], revenue_cate = new int[11];
            foreach (var item in order_detail_done) {
                for (int i = 0; i < array.Count; i++) {
                    if(array[i] == item.Book.Category) {
                        quantity_cate[i] += (int)item.Quantity;
                        revenue_cate[i] += (int)item.Price*(int)item.Quantity;
                    }
                }
            }

            ViewBag.catelist = array;
            ViewBag.quantity_cate = quantity_cate;
            ViewBag.revenue_cate = revenue_cate;
            //detailDAO.GetQuantityOnDate(DateTime.Parse("2022-10-30"), DateTime.Parse("2022-11-01"));
            (ViewBag.label3, ViewBag.value3, ViewBag.value4) = detailDAO.GetQuantityOnDate(DateTime.Today.AddDays(-29), DateTime.Today);   
            (ViewBag.ActiveUser, ViewBag.OrderCount) = detailDAO.overall_static();
            (ViewBag.Quantity_total, ViewBag.Revenue_total) = detailDAO.overall_static_2();
            
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


        public ActionResult EditBook(int? id)
        {
             var catelist = new List<string>() {
            "Motivation",
            "Fantasy",
            "Sci-Fi",
            "Mystery",
            "Manga",
            "Romance",
            "Travel",
            "Guide/How to",
            "Self-help",
            "Textbook",
            "Comic"
            };
            ViewBag.authorlist = authorDAO.GetAuthorList();
            ViewBag.catelist = catelist;
            if (id == null)
            {
                return NotFound();
            }
            var book = bookDAO.GetBookById(id.Value);
            if (book == null) {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                bookDAO.Update(book);
            }
            return RedirectToAction(nameof(Book));
        }
        public IActionResult CreateBook()
        {
            var catelist = new List<string>() {
            "Motivation",
            "Fantasy",
            "Sci-Fi",
            "Mystery",
            "Manga",
            "Romance",
            "Travel",
            "Guide/How to",
            "Self-help",
            "Textbook",
            "Comic"
            };
            ViewBag.authorlist = authorDAO.GetAuthorList();
            ViewBag.catelist = catelist;
            Book book = new Book();
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book) {
            bookDAO.AddNew(book);
            return RedirectToAction(nameof(Book));
            
           
        }

        
        public ActionResult DetailBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = bookDAO.GetBookById(id.Value);
            return View(book);
        }
        public ActionResult EditCustomer(String? Email)
        {
            if (Email == null)
            {
                return NotFound();
            }
            var customer = customerDAO.GetCustomerByEmail(Email);
            if (customer == null) {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerDAO.Update(customer);
            }
            return RedirectToAction(nameof(Customer));
        }
        public IActionResult CreateCustomer()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Customer customer) {
            if (ModelState.IsValid)
            {
                customerDAO.AddNew(customer);
            }
            return RedirectToAction(nameof(Book));
        }
            public ActionResult DeleteCustomer(string? Email){ 
            if (Email == null)
            {
                return NotFound();
            }
            var customer = customerDAO.GetCustomerByEmail(Email);
             if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomers(string Email)
        {  
            try
            {
                customerDAO.Remove(Email);
                return RedirectToAction(nameof(Customer));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction(nameof(DeleteCustomer));
            }
        }


        
        public ActionResult DetailCustomer(String? Email)
        {
            if (Email == null)
            {
                return NotFound();
            }
            var customer = customerDAO.GetCustomerByEmail(Email);
            ViewBag.order = orderDAO.GetOrderByEmail(Email);
            return View(customer);
        }
        public ActionResult DetailOrder(int? id) {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderDAO.GetOrderById(id.Value);
            return View(order);
        }
    }
}