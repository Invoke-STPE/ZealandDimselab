using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingService : GenericService<Booking>
    {
        private UserService userService;

        public BookingService(IDbService<Booking> dbService) : base(dbService)
        {
            
        }

        public List<Booking> GetAllBookings()
        {
            return GetAllObjects();
        }

        public async Task<Booking> GetBookingByKeyAsync(int id)
        {
            return await GetBookingByKeyAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await AddObjectAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await DeleteObjectAsync(await GetBookingByKeyAsync(id));
        }

        public async Task UpdateBookingAsync(int id, Booking booking)
        {
            booking.Id = id;
            await UpdateObjectAsync(booking);
        }
        
        public List<Booking> GetBookingsByEmail(string email)
        {
            foreach (var user in userService.GetUsersAsync())
            {
                if (user.Email == email)
                {
                    return user.GetUserBookings();
                }
            }
            return null;
        }
    }
}
