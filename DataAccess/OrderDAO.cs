using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class OrderDAO
    {
        private readonly BOOKSTOREContext context;
        public OrderDAO(BOOKSTOREContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// Get Order base on Id given
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Order model</returns>
        public Order GetOrderById(int Id)
        {
            return this.context.Orders.Find(Id);
        }

        public Order GetOrderIdWithDetails(int Id) {
            var order = GetOrderById(Id);
            var entry = context.Entry(order);
            entry.Collection(c => c.OrderDetails).Load();
            foreach (var item in order.OrderDetails)
            {
                var e = context.Entry(item);
                e.Reference(c => c.Book).Load();
            }
            return order;
        }

        public void DeleteOrder(int Id)
        {
            var order = GetOrderById(Id);
            if (order != null) context.Orders.Remove(order);
            context.SaveChanges();
        }

        /// <summary>
        /// Delete an order in history base on the owner of the order and order id
        /// </summary>
        /// <param name="CustomerEmail">Customer email</param>
        /// <param name="OrderId">Id of order</param>
        /// <returns>True for successful delete the order, False for otherwise</returns>
        public Boolean DeleteHistoryOrderOwnUser(string CustomerEmail, int OrderId)
        {
            Order res = GetOrderById(OrderId);
            if (res == null) return false;
            if (res.CustomerEmail != CustomerEmail) return false;
            var OrderDetail = this.context.OrderDetails.Where(c => c.OrderId == OrderId).ToList();
            foreach (var item in OrderDetail)
            {
                this.context.OrderDetails.Remove(item);
            }
            this.context.Orders.Remove(res);
            return true;
        }

        /// <summary>
        /// Return a list of order base on status given
        /// </summary>
        /// <param name="Status">the status of order</param>
        /// <returns>List of order</returns>
        public IEnumerable<Order> CheckOrderBaseOnStatus(int Status)
        {
            var result = this.context.Orders.Where(c => c.Status == Status).ToList();
            return result;
        }

        /// <summary>
        /// Return a list of order base on email user and status
        /// </summary>
        /// <param name="CustomerEmail">User email</param>
        /// <param name="Status">Status of the order</param>
        /// <returns>List of order</returns>
        public IEnumerable<Order> checkOrderOnEmailAndStatus(string CustomerEmail, int Status)
        {
            var result = this.context.Orders.Where(c => c.CustomerEmail == CustomerEmail && c.Status == Status).ToList();
            foreach (var item in result)
            {
                var entry = context.Entry(item);
                entry.Reference(d => d.Address).Load();
            }
            return result;
        }

        /// <summary>
        /// Check if any orders that use the address id
        /// </summary>
        /// <param name="AddressId"></param>
        /// <returns>True for exist, False for otherwise</returns>
        public Boolean checkIfUseAddress(int AddressId)
        {
            var result = this.context.Orders.Where(c => c.AddressId == AddressId).ToList();
            if (result.Count != 0) return true;
            return false;
        }

        /// <summary>
        /// Change the status of a specific order base on order Id
        /// </summary>
        /// <param name="OrderId">Id of order</param>
        /// <param name="Status">Status want to change</param>
        public void CheckOrder(int OrderId, int Status)
        {
            var order = GetOrderById(OrderId);
            order.Status = Status;
            this.context.Orders.Update(order);
            this.context.SaveChanges();
        }

        public void AddOrder(Order order) {
            order.OrderId = context.Orders.ToList().Max(c => c.OrderId) + 1;
            context.Add(order);
            context.SaveChanges();
        }
    }
}