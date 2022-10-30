using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Group2_BookStore.DB;
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
        
    }
}