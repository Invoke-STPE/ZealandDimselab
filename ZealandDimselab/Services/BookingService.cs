using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingService
    {
        private UserService UserService;
        private readonly IBookingDb dbService;

        public BookingService(IBookingDb dbService)
        {
            this.dbService = dbService;
        }

        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            await dbService.GetObjectsAsync();
            return await dbService.GetObjectsAsync();
        }

        //public List<Booking> GetAllBookingsTest()
        //{
        //    return dbContext;
        //}

        public async Task<Booking> GetBookingByKeyAsync(int id)
        {
            return await dbService.GetObjectByKeyAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await dbService.AddObjectAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            Booking booking = await GetBookingByKeyAsync(id);
            await dbService.DeleteObjectAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking updatedbooking)
        {
            await dbService.UpdateObjectAsync(updatedbooking);
        }
        
        public async Task<List<Booking>> GetBookingsByEmailAsync(string email) // TODO Pretty sure this doesn't work
        {
            List<Booking> userBookings = new List<Booking>();
            foreach (Booking booking in await GetAllBookings())
            {
                if (booking.User.Email.ToLower() == email.ToLower())
                {
                    userBookings.Add(booking);
                    
                }
            }
            return userBookings;
        }

        public async Task ReturnedBooking(int id)
        {
            Booking booking = await GetBookingByKeyAsync(id);
            booking.Returned = true;
            await dbService.UpdateObjectAsync(booking);
        }
    }
}
