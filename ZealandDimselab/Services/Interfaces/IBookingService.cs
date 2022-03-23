using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services.Interfaces
{
    public interface IBookingService
    {
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task<List<BookedItem>> GetAllBookedItemsAsync();
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByKeyAsync(int id);
        Task<List<Booking>> GetBookingsByEmailAsync(string email);
        Task ReturnedBooking(int id);
        Task UpdateBookingAsync(Booking updatedBooking);
    }
}