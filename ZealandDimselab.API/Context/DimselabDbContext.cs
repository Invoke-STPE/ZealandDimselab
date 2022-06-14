using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Lib.JuntionTables;

namespace ZealandDimselab.API.Context
{
    public class DimselabDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingItem> BookingItems { get; set; }

        public DimselabDbContext()
        {

        }


        public DimselabDbContext(DbContextOptions<DimselabDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingItem>().HasKey(bi => new { bi.BookingId, bi.ItemId });
        }
    }
}