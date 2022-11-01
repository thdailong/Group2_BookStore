using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Group2_BookStore.DB;
using Group2_BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Group2_BookStore.Controllers
{

    public class UserController : Controller
    {
        private readonly CustomerDAO customerDAO;

        public UserController(BOOKSTOREContext db)
        {
            customerDAO = new CustomerDAO(db);
        }
        public IActionResult index()
        {
            var customerEmail = "anhtn@fpt.edu.vn";
            
            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            ViewBag.customer = cus;
            DateTime date = (DateTime)cus.DateOfBirth;
            var data = date.ToString("dd-MM-yyyy");
            ViewBag.date = data;
            return View();
        }

        public IActionResult ChangeInfo()
        {
            var customerEmail = "anhtn@fpt.edu.vn";

            var cus = customerDAO.GetCustomerByEmail(customerEmail);

            return View(cus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Change(Customer customer)
        {
            var customerEmail = "anhtn@fpt.edu.vn";
            
            if (customer.CustomerEmail != customerEmail) return NotFound();
            var cus = customerDAO.GetCustomerByEmail(customerEmail);

            customer.Status = cus.Status;
            customer.Password = cus.Password;

            if (ModelState.IsValid) {
                customerDAO.Update(customer);
                return RedirectToAction("Index");
            }
            ViewBag.message = "Fail to update info";
            return RedirectToAction("ChangeInfo");
        }
        
        public IActionResult ChangePassword()
        {
            var customerEmail = "anhtn@fpt.edu.vn";            

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string oldPass, string newPass, string confirmPass)
        {
            var customerEmail = "anhtn@fpt.edu.vn";            

            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            if (cus.Password != oldPass) {
                ViewBag.message = "Old password did not correct";
                return View();
            }
            if (newPass != confirmPass) {
                ViewBag.message = "Confirm password did not equal to new password";
                return View();
            }
            if (oldPass.Length < 6 || oldPass.Length > 20 || newPass.Length < 6 || newPass.Length > 20 || confirmPass.Length < 6 || confirmPass.Length > 20) {
                ViewBag.message = "Password should be from 6 to 20 characters";
                return View();
            }

            cus.Password = newPass;
            try {
                customerDAO.Update(cus);
                ViewBag.success = "Change password successfully";
            }
            catch (Exception ex) {
                ViewBag.message = ex.Message;
            }

            return View();
        }

        public IActionResult Address()
        {
            var customerEmail = "anhtn@fpt.edu.vn";            

            var cus = customerDAO.GetCustomerByEmail(customerEmail);
            var listAddress = cus.Addresses;
            ViewBag.listAddress = listAddress;
            

            return View();
        }


        
        
    }
}