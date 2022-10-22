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