using Microsoft.EntityFrameworkCore;

namespace ZealandDimselab.Models
{
    public interface IDimselabDbContext
    {
        DbSet<BookingItem> BookingItems { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<User> Users { get; set; }
    }
}