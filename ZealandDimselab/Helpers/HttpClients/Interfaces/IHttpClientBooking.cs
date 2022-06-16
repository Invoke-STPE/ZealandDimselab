using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public interface IHttpClientBooking
    {
        Task AddBookingAsync(BookingDto booking);
        Task<List<BookedItemDto>> GetAllBookedItemsAsync();
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<List<BookingDto>> GetBookingsByEmailAsync(string email);
        Task ReturnedBooking(int id);
    }
}