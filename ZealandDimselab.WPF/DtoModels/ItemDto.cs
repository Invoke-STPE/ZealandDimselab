using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZealandDimselab.WPF.DtoModels
{
    public class ItemDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("imageName")]

        public string ImageName { get; set; }
        [JsonPropertyName("categoryId")]

        public int CategoryId { get; set; }
        [JsonPropertyName("category")]

        public CategoryDto Category { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("stock")]

        public int Stock { get; set; }
        

        private int _bookingQuantity;
        [JsonPropertyName("bookingQuantity")]

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

        public override string ToString()
        {
            return Name;
        }
    }
}
