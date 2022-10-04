using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zealand.Dimselab.Domain.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
        //public List<BookingItemModel> BookingItems { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string Details { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
