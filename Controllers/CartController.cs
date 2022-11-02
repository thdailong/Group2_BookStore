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
    public class CartController : Controller
    {
        private readonly CartDAO cartDAO;
        private readonly AddressDAO addressDAO;
        private readonly CustomerDAO customerDAO;
        private readonly OrderDAO orderDAO;
        private readonly OrderDetailDAO orderDetailDAO;

        public CartController(BOOKSTOREContext db)
        {
            cartDAO = new CartDAO(db);
            addressDAO = new AddressDAO(db);
            customerDAO = new CustomerDAO(db);
            orderDAO = new OrderDAO(db);
            orderDetailDAO = new OrderDetailDAO(db);
        }

        public IActionResult Index()
        {
            //Email from session(Wait for update)
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var mylist = cartDAO.GetCartsOnCusEmail(customerEmail);
            ViewBag.mylist = mylist;
            return View();
        }

        public string getUser()
        {
            var userMail = HttpContext.Session.GetString("CustomerEmail");
            return userMail;
        }

        public IActionResult UpdateCart(int[] CartId, int[] quantity)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            for (int i = 0; i < CartId.Length; ++i)
            {
                var find = cartDAO.GetCartOnId(CartId[i]);
                if (find == null) return NotFound();
                if (find.CustomerEmail != customerEmail) return NotFound();
            }

            for (int i = 0; i < CartId.Length; ++i)
            {
                var find = cartDAO.GetCartOnId(CartId[i]);
                find.Quantity = quantity[i];
                cartDAO.UpdateCart(find);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCart(int CartId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var find = cartDAO.GetCartOnId(CartId);
            if (find != null && find.CustomerEmail == customerEmail) cartDAO.DeleteCart(find);
            HttpContext.Session.SetInt32("NumberItem", cartDAO.GetCartsOnCusEmail(customerEmail).Count());

            return RedirectToAction("Index");
        }

        public IActionResult AddBookToCart(int BookId, int Quantity)
        {
            var customerEmail = getUser();
            if (customerEmail == null)
            {
                TempData["Message"] = "You need to login so as to add book to cart";
                return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
            }

            cartDAO.AddBookToCart(BookId, Quantity, customerEmail);
            HttpContext.Session.SetInt32("NumberItem", cartDAO.GetCartsOnCusEmail(customerEmail).Count());
            TempData["Success"] = "Add book successfully";
            return RedirectToAction("BookDetail", "Book", new { BookId = BookId });
        }

        public IActionResult Checkout()
        {
            var customerEmail = getUser();
            if (customerEmail == null)
            {
                TempData["Message"] = "You need to login to make checkout";
                return RedirectToAction("Index", "Home");
            }
            var list = cartDAO.GetCartsOnCusEmail(customerEmail);

            if (list.Count() == 0)
            {
                TempData["Message"] = "There are no items in cart to checkout";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.list = list;
            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            var listAddress = cus.Addresses;

            ViewBag.listAddress = listAddress;
            ViewBag.Check = listAddress.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(int? AddressId)
        {
            var customerEmail = getUser();
            if (customerEmail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (AddressId == null)
            {
                return RedirectToAction("Checkout");
            }
            var order = new Order();
            order.OrderDateTime = DateTime.Now;
            order.CustomerEmail = customerEmail;
            order.AddressId = AddressId.Value;
            order.Status = 1;
            orderDAO.AddOrder(order);
            var list = cartDAO.GetCartsOnCusEmail(customerEmail);
            foreach (var item in list)
            {
                var x = new OrderDetail();
                x.OrderId = order.OrderId;
                x.BookId = item.BookId;
                x.Quantity = item.Quantity;
                x.Price = item.Book.Price;
                orderDetailDAO.Add(x);
                cartDAO.DeleteCart(item);
            }
            HttpContext.Session.SetInt32("NumberItem", 0);

            return RedirectToAction("Orders", "User");
        }


    }
}