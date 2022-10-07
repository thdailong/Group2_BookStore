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
        public DbSet<customer> Customers {get; set;}
        public DbSet<address> addresses {get; set;}
        public DbSet<author> authors {get; set;}
        public DbSet<award> awards {get; set;}
        public DbSet<book_award> book_awards {get; set;}
        public DbSet<book> books {get; set;}
        public DbSet<cart> carts {get; set;}
        public DbSet<comment> comments {get; set;}
        public DbSet<order_detail> order_details {get; set;}
        public DbSet<order> orders {get; set;}
        public DbSet<rate> rates {get; set;}
    }
}