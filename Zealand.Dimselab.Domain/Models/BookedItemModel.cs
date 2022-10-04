using System;
using System.Collections.Generic;
using System.Text;

namespace Zealand.Dimselab.Domain.Models
{
    public class BookedItemModel
    {
        public ItemModel Item { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public UserModel User { get; set; }
        public int BookingId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
    }
}
