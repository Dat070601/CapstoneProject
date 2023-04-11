using BookStore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DAL
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
            
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options) 
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Image>().HasOne(e => e.Shop).WithMany().OnDelete(DeleteBehavior.Restrict);
        //}

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Address> Addresses  { get; set; }
        public virtual DbSet<Author> Authors  { get; set; }
        public virtual DbSet<Bank> Banks  { get; set; }
        public virtual DbSet<BankType> BankTypes  { get; set; }
        public virtual DbSet<Book> Books  { get; set; }
        public virtual DbSet<BookPrice> BookPrices  { get; set; }            
        public virtual DbSet<Cart> Carts  { get; set; }
        public virtual DbSet<Credential> Credentials  { get; set; }
        public virtual DbSet<HistoryTransaction> HistoryTransactions  { get; set; }
        public virtual DbSet<Image> Images  { get; set; }
        public virtual DbSet<Order> Orders  { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails  { get; set; }
        public virtual DbSet<Payment> Payments  { get; set; }
        public virtual DbSet<Publisher> Publishers  { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens  { get; set; }
        public virtual DbSet<Review> Reviews  { get; set; }
        public virtual DbSet<Role> Roles  { get; set; }
        public virtual DbSet<Shop> Shops  { get; set; }
        public virtual DbSet<Status> Statuses  { get; set; }
        public virtual DbSet<UserGroup> UserGroups  { get; set; }
    }
}
