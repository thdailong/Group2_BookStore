using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.DB;
using Group2_BookStore.Models;

namespace Group2_BookStore.DataAccess
{
    public class RateDAO
    {
        private readonly BOOKSTOREContext context; 
        public RateDAO(BOOKSTOREContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// Get Rate base on the Id of Rate
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Rate model</returns>
        public Rate GetRateOnId(int Id) {
            var res = this.context.Rates.Find(Id);
            return res;
        }
        
        /// <summary>
        /// Add Rate from parameter to database
        /// </summary>
        /// <param name="Rate">Rate model</param>
        public void AddRate(Rate Rate) {
            this.context.Rates.Add(Rate);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Update Rate from parameter to database
        /// </summary>
        /// <param name="Rate">Rate model</param>
        public void UpdateRate(Rate Rate) {
            this.context.Rates.Update(Rate);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Delete Rate base on Id given
        /// </summary>
        /// <param name="Id">Rate Id</param>
        public void DeleteRate(int Id) {
            var res = GetRateOnId(Id);
            this.context.Rates.Remove(res);
        }

        /// <summary>
        /// Delete a Rate with Id given but only from the user that make this Rate
        /// </summary>
        /// <param name="CustomerEmail">The email of the user</param>
        /// <param name="Id">Id of the Rate</param>
        /// <returns>True for delete successful, False for other wise</returns>
        public Boolean DeleteRateFromOwnUser(string CustomerEmail, int Id) {
            var res = GetRateOnId(Id);
            if (res.CustomerEmail == CustomerEmail) {
                DeleteRate(Id);
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Get percent rate of a book base on Id given
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>A float value which is the result</returns>
        public float getRateTotalBaseOnIdBook(int bookId) {
            var res = this.context.Rates.Where(c => c.BookId == bookId).ToList();
            int numberStar = 0, numberStarGet = 0;
            foreach (var item in res)
            {
                numberStar+=5;
                numberStarGet+=(int)item.AmountStar;
            }
            return (float)numberStarGet/numberStar;
        }
    }
}