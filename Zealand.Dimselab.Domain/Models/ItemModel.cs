using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zealand.Dimselab.Domain.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string ImageName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
         public int Quantity { get; set; }
       public int Stock { get; set; }
        private int _bookingQuantity;
        public int BookingQuantity
        {
            get { return _bookingQuantity; }
            set
            {
                if (value <= Stock)
                {
                    _bookingQuantity = value;
                }
            }
        }

        //public ICollection<BookingItem> BookingItems { get; set; }

        public ItemModel()
        {
        }
       
    }
}
