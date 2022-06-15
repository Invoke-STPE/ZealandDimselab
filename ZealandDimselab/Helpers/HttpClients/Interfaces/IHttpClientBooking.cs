using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public interface IHttpClientBooking
    {
        Task AddBookingAsync(Booking booking);
        Task<List<BookedItem>> GetAllBookedItemsAsync();
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByEmailAsync(string email);
        Task ReturnedBooking(int id);
    }
}