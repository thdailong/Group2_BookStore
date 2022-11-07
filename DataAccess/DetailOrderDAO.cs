using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class DetailOrderDAO
    {
        private readonly BOOKSTOREContext context; 
        public DetailOrderDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// Get List of order details base on Id given
        /// </summary>
        /// <param name="Id">Id of order</param>
        /// <returns>List of order details</returns>
        public IEnumerable<OrderDetail> GetListOrderDetailsOnId(int Id) {
            var res = this.context.OrderDetails.Where(c => c.OrderId == Id).ToList();
            return res;
        }
        public IEnumerable<OrderDetail> GetListOrderDetailOnStatus(int status) {
            var res = this.context.OrderDetails.Where(c => c.Order.Status == status).ToList();
            this.context.Books.ToList();
            return res;
        }

        public (List<String>list1, List<int> list2, List<int> list3) GetQuantityOnDate(DateTime from_date, DateTime to_date) {
            var res = this.context.OrderDetails
            .Where(c => c.Order.OrderDateTime >= from_date && c.Order.OrderDateTime <= to_date && c.Order.Status == 4)
            .GroupBy(x => x.Order.OrderDateTime)
            .Select(x => new { date = x.Key, quantity = x.Sum(c => c.Quantity), revenue = x.Sum(c => c.Quantity*c.Price)})
            .OrderBy(x => x.date);
            var ls = new List<String>();
            var ls1 = new List<int>();
            var ls2 = new List<int>();
            foreach (var item in res)
            {
                ls.Add(((DateTime)item.date).ToString("MMM-dd"));
                ls1.Add((int)item.quantity);
                ls2.Add((int)item.revenue);
                System.Console.WriteLine(((DateTime)item.date).ToString("MMM-dd"));
                System.Console.WriteLine((int)item.quantity);
            }
            return (ls, ls1, ls2); 
        }
        public (int, int) overall_static() {
            int a1 = 0, a2 = 0, a3 = 0;
            a1 = this.context.Orders.Select(c => c.CustomerEmail).Distinct().Count();
            a2 = this.context.Orders.Select(c => c.OrderId).Distinct().Count();
            return (a1, a2);
        }
        public (int, int) overall_static_2() {
            int a1 = 0, a2 = 0;
            a1 = this.context.OrderDetails.Select(c => (int)c.Quantity).Sum();
            a2 = this.context.OrderDetails.Select(c => (int)c.Quantity*(int)c.Price).Sum();
            return (a1, a2);
        }

        
        /// <summary>
        /// Get an order detail on book Id and Order Id
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="OrderId"></param>
        /// <returns>Order detail model</returns>
        public OrderDetail GetOrderDetail(int BookId, int OrderId) {
            var res = this.context.OrderDetails.Where(c => c.BookId == BookId && c.OrderId == OrderId).FirstOrDefault();
            return res;
        }


        /// <summary>
        /// Delete an order detail on book Id and Order Id
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="OrderId"></param>
        public void DeleteOnBookIdAndOrderId(int BookId, int OrderId) {
            var res = GetOrderDetail(BookId, OrderId);
            this.context.OrderDetails.Remove(res);
            this.context.SaveChanges();
        }
    }
}