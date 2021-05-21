using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class BookedItem: IComparable
    {
        public Item Item { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

        public BookedItem()
        {

        }

        public BookedItem(Item item, DateTime bookingDate, DateTime returnDate, User user, bool status, int quantity)
        {
            Item = item;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            User = user;
            if (status) Status = "Returned";
            else Status = "Not Returned";
            Quantity = quantity;
        }
    }
}
