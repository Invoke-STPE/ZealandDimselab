using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class BookedItem
    {
        public Item Item { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }

        public BookedItem()
        {

        }

        public BookedItem(Item item, DateTime bookingDate, DateTime returnDate, User user)
        {
            Item = item;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            User = user;
        }
    }
}
