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
        /// Add rate to database
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="CustomerEmail"></param>
        /// <param name="AmountStar"></param>        
        public void addRate(int BookId, string CustomerEmail, int AmountStar) {
            var x = getRateOnBookAndId(BookId, CustomerEmail);
            if (x != 0) {
                updateRate(BookId, CustomerEmail, AmountStar);
                return;
            }

            var rate = new Rate();
            rate.BookId = BookId;
            rate.AmountStar = AmountStar;
            rate.CustomerEmail = CustomerEmail;
            rate.RateId = context.Rates.Max(c => c.RateId) + 1;
            context.Rates.Add(rate);
            context.SaveChanges();
        }

        /// <summary>
        /// Update rate to database
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="CustomerEmail"></param>
        /// <param name="AmountStar"></param>
        public void updateRate(int BookId, string CustomerEmail, int AmountStar) {
            var rate = getEntityOnBookAndId(BookId, CustomerEmail);
            if (rate == null) return;

            rate.BookId = BookId;
            rate.AmountStar = AmountStar;
            rate.CustomerEmail = CustomerEmail;
            context.Rates.Update(rate);
            context.SaveChanges();
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
        /// Get rate from user and book
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="CustomerEmail"></param>
        /// <returns></returns>
        public int getRateOnBookAndId(int BookId, string CustomerEmail) {
            var x = this.context.Rates.SingleOrDefault(c => c.BookId == BookId && c.CustomerEmail == CustomerEmail);
            if (x == null) {
                return 0;
            }
            return (int)x.AmountStar;
        }

        public Rate getEntityOnBookAndId(int BookId, string CustomerEmail) {
            var x = this.context.Rates.SingleOrDefault(c => c.BookId == BookId && c.CustomerEmail == CustomerEmail);
            return x;
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
        public (int, int) getRateTotalBaseOnIdBook(int bookId) {
            var res = this.context.Rates.Where(c => c.BookId == bookId).ToList();
            int numberStar = 0, numberStarGet = 0;
            foreach (var item in res)
            {
                numberStar+=5;
                numberStarGet+=(int)item.AmountStar;
            }
            return (numberStarGet, numberStar);
        }

        public int getRateOnIdAndStar(int bookId, int amountStar) {
            var res = this.context.Rates.Where(c => c.BookId == bookId && c.AmountStar == amountStar).ToList();
            return res.Count();
        }
    }
}