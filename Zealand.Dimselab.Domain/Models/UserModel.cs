using System;
using System.Collections.Generic;
using System.Text;

namespace Zealand.Dimselab.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<BookingModel> Bookings { get; set; }
        public ICollection<BookingModel> GetUserBookings()
        {
            return Bookings;
        }
    }
}
