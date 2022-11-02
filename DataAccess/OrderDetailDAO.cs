using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class OrderDetailDAO
    {
        private readonly BOOKSTOREContext context;
        public OrderDetailDAO(BOOKSTOREContext _context)
        {
            this.context = _context;
        }

        public void Add(OrderDetail orderDetail) {
            orderDetail.OrderDetailId = context.OrderDetails.ToList().Max(c => c.OrderDetailId) + 1;
            context.Add(orderDetail);
            context.SaveChanges();
        }
    }
}