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
        private UserService UserService;

        public BookingService(IDbService<Booking> dbService) : base(dbService)
        {
        }

        public List<Booking> GetAllBookings()
        {
            return GetAllObjects();
        }

        //public List<Booking> GetAllBookingsTest()
        //{
        //    return dbContext;
        //}

        public async Task<Booking> GetBookingByKeyAsync(int id)
        {
            return await GetObjectByKeyAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await AddObjectAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await DeleteObjectAsync(await GetBookingByKeyAsync(id));
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await UpdateObjectAsync(booking);
        }
        
        public List<Booking> GetBookingsByEmail(string email)
        {
            List<Booking> userBookings = new List<Booking>();
            foreach (Booking booking in GetAllBookings())
            {
                if (booking.User.Email.ToLower() == email.ToLower())
                {
                    userBookings.Add(booking);
                    return userBookings;
                }
            }
            return null;
        }
    }
}
