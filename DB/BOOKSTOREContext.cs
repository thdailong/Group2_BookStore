using System;
using Group2_BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Group2_BookStore.DB
{
    public partial class BOOKSTOREContext : DbContext
    {
       
        public BOOKSTOREContext(DbContextOptions<BOOKSTOREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAward> BookAwards { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId)
                    .ValueGeneratedNever()
                    .HasColumnName("address_id");

                entity.Property(e => e.Address1)
                    .HasMaxLength(300)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.ShippingNumberPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("shipping_number_phone");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK_address_customer");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.AuthorId)
                    .ValueGeneratedNever()
                    .HasColumnName("author_id");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .HasColumnName("country");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Detail)
                    .HasMaxLength(500)
                    .HasColumnName("detail");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Award>(entity =>
            {
                entity.HasKey(e => e.Award1);

                entity.ToTable("award");

                entity.Property(e => e.Award1)
                    .ValueGeneratedNever()
                    .HasColumnName("award");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookId)
                    .ValueGeneratedNever()
                    .HasColumnName("book_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.BookFormat)
                    .HasMaxLength(50)
                    .HasColumnName("book_format");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.DetailBook)
                    .HasMaxLength(500)
                    .HasColumnName("detail_book");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PageNumber).HasColumnName("page_number");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Publisher)
                    .HasMaxLength(100)
                    .HasColumnName("publisher");

                entity.Property(e => e.PulishDate)
                    .HasColumnType("date")
                    .HasColumnName("pulish_date");

                entity.Property(e => e.Size)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_book_author");
            });

            modelBuilder.Entity<BookAward>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("book_award");

                entity.Property(e => e.AwardId).HasColumnName("award_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.HasOne(d => d.Award)
                    .WithMany()
                    .HasForeignKey(d => d.AwardId)
                    .HasConstraintName("FK_book_award_award");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_book_award_book");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .ValueGeneratedNever()
                    .HasColumnName("cart_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_cart_book");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK_cart_customer");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommendId);

                entity.ToTable("comment");

                entity.Property(e => e.CommendId)
                    .ValueGeneratedNever()
                    .HasColumnName("commend_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.ContentComment)
                    .HasMaxLength(500)
                    .HasColumnName("content_comment");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.TimeComment)
                    .HasColumnType("datetime")
                    .HasColumnName("time_comment");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK_comment_customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerEmail)
                    .HasName("PK_customer_1");

                entity.ToTable("customer");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.Property(e => e.OrderDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK_order_customer");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_detail");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_detail_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_order_detail_book");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_order_detail_order");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("rate");

                entity.Property(e => e.RateId)
                    .ValueGeneratedNever()
                    .HasColumnName("rate_id");

                entity.Property(e => e.AmountStar).HasColumnName("amount_star");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_email");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_rate_book");

                entity.HasOne(d => d.CustomerEmailNavigation)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.CustomerEmail)
                    .HasConstraintName("FK_rate_customer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
