using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingService
    {
        private UserService UserService;
        private List<Booking> bookings;
        private readonly IDbService<Booking> dbService;

        public BookingService(IDbService<Booking> dbService)
        {
            this.dbService = dbService;
            bookings = dbService.GetObjectsAsync().Result.ToList();
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
            return bookings.SingleOrDefault(u => u.Id == id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            bookings.Add(booking);
            await dbService.AddObjectAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            Booking booking = await GetBookingByKeyAsync(id);
            bookings.Remove(booking);
            await dbService.DeleteObjectAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking updatedbooking)
        {
            foreach (var booking in bookings)
            {
                if (booking.Id == updatedbooking.Id)
                {
                    booking.BookingDate = updatedbooking.BookingDate;
                    booking.ReturnDate = updatedbooking.ReturnDate;
                    booking.Details = updatedbooking.Details;
                    booking.User = updatedbooking.User;
                    booking.BookingItems = updatedbooking.BookingItems;
                }
            }
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
    }
}
