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

    public class UserController : Controller
    {
        private readonly CustomerDAO customerDAO;
        private readonly AddressDAO addressDAO;
        private readonly OrderDAO orderDAO;
        private readonly CartDAO cartDAO;
        private readonly FavoriteDAO favoriteDAO;

        public UserController(BOOKSTOREContext db)
        {
            customerDAO = new CustomerDAO(db);
            addressDAO = new AddressDAO(db);
            orderDAO = new OrderDAO(db);
            cartDAO = new CartDAO(db);
            favoriteDAO = new FavoriteDAO(db);
        }
        public IActionResult index()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            ViewBag.customer = cus;
            DateTime date = (DateTime)cus.DateOfBirth;
            var data = date.ToString("dd-MM-yyyy");
            ViewBag.date = data;
            return View();
        }

        [HttpPost]
        public IActionResult login(string CustomerEmail, string Password, string Path)
        {

            var customerEmail = getUser();
            if (customerEmail != null) return RedirectToAction("Index", "Home");
            var cus = customerDAO.CustomerLogin(CustomerEmail, Password);
            if (cus == null)
            {
                TempData["MessageLogin"] = "Wrong email or password";
                return RedirectToAction("LoginFull");
            }
            HttpContext.Session.SetInt32("Status", cus.Status);
            HttpContext.Session.SetString("CustomerEmail", cus.CustomerEmail);
            HttpContext.Session.SetString("Name", cus.Name);
            HttpContext.Session.SetInt32("NumberItem", cartDAO.GetCartsOnCusEmail(CustomerEmail).Count());
            HttpContext.Session.SetInt32("NumberFavorite", favoriteDAO.getFavoriteOnCustomer(CustomerEmail).Count());

            return Redirect(Path);
        }

        public IActionResult LoginFull()
        {
            var customerEmail = getUser();
            if (customerEmail != null) return RedirectToAction("Index", "Home");

            return View();
        }


        public IActionResult register()
        {
            var customerEmail = getUser();
            if (customerEmail != null) return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult registerr(Customer customer, string RepeatPassword)
        {
            var customerEmail = getUser();
            if (customerEmail != null) return RedirectToAction("Index", "Home");
            customer.Status = 1;
            if (ModelState.IsValid)
            {
                if (customer.Password != RepeatPassword)
                {
                    TempData["Message"] = "Repeat password and password login is not the same";
                    return RedirectToAction("register");
                }
                var cus = customerDAO.GetCustomerByEmail(customer.CustomerEmail);
                if (cus != null)
                {
                    TempData["Message"] = "Email have already exist!";
                    return RedirectToAction("register");
                }
                customerDAO.AddNew(customer);
                HttpContext.Session.SetInt32("Status", customer.Status);
                HttpContext.Session.SetString("CustomerEmail", customer.CustomerEmail);
                HttpContext.Session.SetString("Name", customer.Name);
                HttpContext.Session.SetInt32("NumberItem", cartDAO.GetCartsOnCusEmail(customer.CustomerEmail).Count());
                return RedirectToAction("index");
            }
            return RedirectToAction("register");
        }


        public IActionResult logout()
        {
            HttpContext.Session.Remove("Status");
            HttpContext.Session.Remove("CustomerEmail");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("NumberItem");

            return RedirectToAction("Index", "Home");
        }

        public string getUser()
        {
            var userMail = HttpContext.Session.GetString("CustomerEmail");
            return userMail;
        }

        public IActionResult ChangeInfo()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var cus = customerDAO.GetCustomerByEmail(customerEmail);

            return View(cus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Change(Customer customer)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            if (customer.CustomerEmail != customerEmail) return NotFound();
            var cus = customerDAO.GetCustomerByEmail(customerEmail);

            customer.Status = cus.Status;
            customer.Password = cus.Password;

            if (ModelState.IsValid)
            {
                customerDAO.Update(customer);
                return RedirectToAction("Index");
            }
            ViewBag.message = "Fail to update info";
            return RedirectToAction("ChangeInfo");
        }

        public IActionResult ChangePassword()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string oldPass, string newPass, string confirmPass)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            if (cus.Password != oldPass)
            {
                ViewBag.message = "Old password did not correct";
                return View();
            }
            if (newPass != confirmPass)
            {
                ViewBag.message = "Confirm password did not equal to new password";
                return View();
            }
            if (oldPass.Length < 6 || oldPass.Length > 20 || newPass.Length < 6 || newPass.Length > 20 || confirmPass.Length < 6 || confirmPass.Length > 20)
            {
                ViewBag.message = "Password should be from 6 to 20 characters";
                return View();
            }

            cus.Password = newPass;
            try
            {
                customerDAO.Update(cus);
                ViewBag.success = "Change password successfully";
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
            }

            return View();
        }

        public IActionResult Address()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            var listAddress = cus.Addresses;
            ViewBag.listAddress = listAddress;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertAddress(Address address)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            address.CustomerEmail = customerEmail;
            address.AddressId = 0;
            if (ModelState.IsValid)
            {
                addressDAO.AddAddress(address);
            }

            return RedirectToAction("Address");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertAddressFromCheckOut(Address address)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            address.CustomerEmail = customerEmail;
            address.AddressId = 0;
            if (ModelState.IsValid)
            {
                addressDAO.AddAddress(address);
            }

            return RedirectToAction("Checkout", "Cart");
        }

        public IActionResult DeleteAddress(int AddressId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var address = addressDAO.getAddressById(AddressId);
            if (address == null) return NotFound();
            if (address.CustomerEmail != customerEmail) return NotFound();
            if (orderDAO.checkIfUseAddress(AddressId)) TempData["Message"] = "Can not delete due to some order use that address";
            else addressDAO.DeleteAddress(AddressId);
            return RedirectToAction("Address");
        }

        public IActionResult EditAddress(int AddressId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var address = addressDAO.getAddressById(AddressId);
            if (address == null) return NotFound();
            if (address.CustomerEmail != customerEmail) return NotFound();

            return View(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAddress(Address address)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var _address = addressDAO.getAddressById(address.AddressId);
            if (_address == null) return NotFound();
            if (customerEmail != _address.CustomerEmail)
            {
                TempData["Message"] = "Can not modify address";
                return RedirectToAction("Address");
            }
            address.CustomerEmail = customerEmail;
            if (ModelState.IsValid)
            {
                addressDAO.UpdateAddress(address);
            }

            return RedirectToAction("Address");
        }

        public IActionResult Orders()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var listOrderCancel = orderDAO.checkOrderOnEmailAndStatus(customerEmail, 0);
            listOrderCancel.Reverse();
            var listOrderWait = orderDAO.checkOrderOnEmailAndStatus(customerEmail, 1);
            listOrderWait.Reverse();
            var listOrderConfirm = orderDAO.checkOrderOnEmailAndStatus(customerEmail, 2);
            listOrderConfirm.Reverse();
            var listOrderDelivering = orderDAO.checkOrderOnEmailAndStatus(customerEmail, 3);
            listOrderDelivering.Reverse();
            var listOrderDone = orderDAO.checkOrderOnEmailAndStatus(customerEmail, 4);
            listOrderDone.Reverse();

            ViewBag.listOrderCancel = listOrderCancel;
            ViewBag.listOrderWait = listOrderWait;
            ViewBag.listOrderConfirm = listOrderConfirm;
            ViewBag.listOrderDelivering = listOrderDelivering;
            ViewBag.listOrderDone = listOrderDone;

            return View();
        }

        public IActionResult DeleteOrder(int OrderId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var order = orderDAO.GetOrderById(OrderId);
            if (order.CustomerEmail != customerEmail && order.Status != 1) return NotFound();
            orderDAO.DeleteOrder(OrderId);

            return RedirectToAction("Orders");
        }

        public IActionResult DetailOrder(int OrderId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var order = orderDAO.GetOrderIdWithDetails(OrderId);
            if (order.CustomerEmail != customerEmail && order.Status != 1) return NotFound();
            var list = order.OrderDetails;
            ViewBag.list = list;
            var date = (DateTime)order.OrderDateTime;
            var data = date.ToString("dd-MM-yyyy");
            ViewBag.data = data;

            return View();
        }

        public IActionResult Favorite()
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            var list = favoriteDAO.getFavoriteOnCustomer(customerEmail);
            ViewBag.ListFavorite = list;

            return View();
        }

        public IActionResult DeleteFavorite(int BookId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            favoriteDAO.deleteFavorite(customerEmail, BookId);

            return RedirectToAction("Favorite");
        }

        public IActionResult AddFavorite(int BookId)
        {
            var customerEmail = getUser();
            if (customerEmail == null) return RedirectToAction("Index", "Home");

            HttpContext.Session.SetInt32("NumberFavorite", favoriteDAO.getFavoriteOnCustomer(customerEmail).Count());
            favoriteDAO.addFavorite(customerEmail, BookId);
            TempData["Success"] = "Add to favorite successfully";

            return RedirectToAction("Favorite");
        }


    }
}