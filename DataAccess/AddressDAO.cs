using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class AddressDAO
    {
        private readonly BOOKSTOREContext context;
        public AddressDAO(BOOKSTOREContext _context)
        {
            this.context = _context;
        }

        /// <summary>
        /// Return an address on given address Id
        /// </summary>
        /// <param name="AddressId"></param>
        /// <returns>address model</returns>
        public Address getAddressById(int AddressId)
        {
            var address = context.Addresses.Find(AddressId);
            return address;
        }

        /// <summary>
        /// Return a list of address model
        /// </summary>
        /// <returns>list address</returns>
        public IEnumerable<Address> getListAddress()
        {
            var list = context.Addresses.ToList();
            return list;
        }

        /// <summary>
        /// Add address model to database
        /// </summary>
        /// <param name="address"></param>
        public void AddAddress(Address address)
        {
            var id = context.Addresses.Max(c => c.AddressId) + 1;
            address.AddressId = id;
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        /// <summary>
        /// Delete address on address Id
        /// </summary>
        /// <param name="addressId"></param>
        public void DeleteAddress(int addressId)
        {
            var address = getAddressById(addressId);
            if (address != null)
            {
                context.Addresses.Remove(address);
                context.SaveChanges();
            }
        }

        public void UpdateAddress(Address address)
        {

            context.Entry(address).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.Set<Address>().Update(address);
            context.SaveChanges();
        }
    }
}