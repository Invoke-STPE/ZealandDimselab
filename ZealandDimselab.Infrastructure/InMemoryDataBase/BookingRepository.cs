using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class BookingRepository : BaseRepository<BookingModel>, IBookingRepository
    {
        private List<BookingModel> _bookingModels;
        public BookingRepository()
        {
            _bookingModels = new List<BookingModel>()
            {
                new BookingModel()
                {
                    Id = 1,
                    BookingDate = DateTime.Now,
                    ReturnDate = DateTime.Today,
                    Details = "Test 1",
                    Returned = false,
                    User = new UserModel() { Bookings = new List<BookingModel>(), Email = "Steven@email.com", Id = 1, Password = "Test", Role = "admin" },
                    UserId = 1
                },
                new BookingModel()
                {
                    Id = 2,
                    BookingDate = DateTime.Now,
                    ReturnDate = DateTime.Today,
                    Details = "Test 2",
                    Returned = false,
                    User = new UserModel() { Bookings = new List<BookingModel>(), Email = "Mike@email.com", Id = 2, Password = "Test", Role = "student" },
                    UserId = 2
                },
                new BookingModel()
                {
                    Id = 3,
                    BookingDate = DateTime.Now,
                    ReturnDate = DateTime.Today,
                    Details = "Test 3",
                    Returned = false,
                    User = new UserModel() { Bookings = new List<BookingModel>(), Email = "Ninette@email.com", Id = 3, Password = "Test", Role = "student" },
                    UserId = 3
                }
            };
        }
        public override async Task<IEnumerable<BookingModel>> GetObjectsAsync()
        {
            return _bookingModels;
        }

        public override async Task<BookingModel> GetObjectByKeyAsync(int bookingId)
        {
            return _bookingModels.SingleOrDefault(b => b.Id == bookingId);
        }

        public async Task<List<BookingModel>> GetBookingsByEmailAsync(string email)
        {
            return _bookingModels.FindAll(b => b.User.Email == email);
        }
        public override async Task<BookingModel> InsertAsync(BookingModel booking)
        {
            booking.Id = _bookingModels.Count + 1;
            booking.Returned = false;
            _bookingModels.Add(booking);
            return booking;

        }

        public override async Task<BookingModel> DeleteAsync(int id)
        {
            var booking = await GetObjectByKeyAsync(id);
            _bookingModels.Remove(booking);
            return booking;
        }

        public override async Task<BookingModel> UpdateAsync(BookingModel booking)
        {

            return null;


        }
    }
}
