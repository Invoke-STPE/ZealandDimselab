using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZealandDimselab.DTO
{
    public class BookedItemDto
    {
        [JsonPropertyName("item")]

        public ItemDto Item { get; set; }
        [JsonPropertyName("bookingDate")]

        public DateTime BookingDate { get; set; }
        [JsonPropertyName("returnDate")]

        public DateTime ReturnDate { get; set; }
        [JsonPropertyName("user")]

        public UserDto User { get; set; }
        [JsonPropertyName("bookingId")]

        public int BookingId { get; set; }
        [JsonPropertyName("status")]

        public string Status { get; set; }
        [JsonPropertyName("quantity")]

        public int Quantity { get; set; }
    }
}
