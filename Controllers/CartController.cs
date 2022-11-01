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

        public CartController(BOOKSTOREContext db)
        {
            cartDAO = new CartDAO(db);
        }

        public IActionResult Index()
        {
            //Email from session(Wait for update)
            var customerEmail = "anhtn@fpt.edu.vn";

            var mylist = cartDAO.GetCartsOnCusEmail(customerEmail);
            ViewBag.mylist = mylist;
            return View();
        }

        public IActionResult UpdateCart(int[] CartId, int[] quantity)
        {
            var customerEmail = "anhtn@fpt.edu.vn";

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
            var customerEmail = "anhtn@fpt.edu.vn";
            
            var find = cartDAO.GetCartOnId(CartId);
            if (find != null && find.CustomerEmail == customerEmail) cartDAO.DeleteCart(find);

            return RedirectToAction("Index");
        }
        


    }
}