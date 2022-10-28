using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace DataAccess
{
    public class CustomerDAO
    {
        private readonly BOOKSTOREContext context; 
        public CustomerDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }
        public IEnumerable<Customer> GetCustomerList()
        {
            var Customers = new List<Customer>();
            try
            {
                Customers = context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Customers;
        }


        public Customer GetCustomerByEmail(String CustomerEmail)
        {
            Customer Customer = null;
            try
            {
                Customer = context.Customers.SingleOrDefault(c => c.CustomerEmail == CustomerEmail);
                if (Customer != null) {
                    var e = context.Entry(Customer);
                    e.Collection(c => c.Orders).Load();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Customer;
        }

        public Customer CustomerLogin(string Email, string Password) {
            try {
                var customer = from Customer in context.Customers 
                    where Customer.CustomerEmail == Email 
                    where Customer.Password == Password 
                    select Customer;
                if (customer == null) {
                    return null;
                }
                return customer.FirstOrDefault();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void AddNew(Customer Customer)
        {
            try
            {
                Customer _Customer = GetCustomerByEmail(Customer.CustomerEmail);
                if (_Customer == null)
                {
                    context.Customers.Add(Customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Customer is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Customer Customer)
        {
            try
            {
                Customer _Customer = GetCustomerByEmail(Customer.CustomerEmail);
                if (_Customer != null)
                {
                    context.Customers.Update(Customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Customer does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(String Email)
        {
            try
            {
                Customer Customer = GetCustomerByEmail(Email);
                if (Customer != null)
                {
                    context.Customers.Remove(Customer);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Customer does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean RegisterOrNot(string Email) {
            Customer Customer = null;
            try
            {
                Customer = context.Customers.SingleOrDefault(c => c.CustomerEmail == Email);
                if (Customer != null) {
                    var e = context.Entry(Customer);
                    e.Collection(c => c.Orders).Load();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}