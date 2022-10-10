using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group2_BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Group2_BookStore.DB
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) {

        }
        public DbSet<customer> customer {get; set;}
        public DbSet<address> address {get; set;}
        public DbSet<author> author {get; set;}
        public DbSet<award> award {get; set;}
        public DbSet<book_award> book_award {get; set;}
        public DbSet<book> book {get; set;}
        public DbSet<cart> cart {get; set;}
        public DbSet<comment> comment {get; set;}
        public DbSet<order_detail> order_detail {get; set;}
        public DbSet<order> order {get; set;}
        public DbSet<rate> rate {get; set;}
    }
}