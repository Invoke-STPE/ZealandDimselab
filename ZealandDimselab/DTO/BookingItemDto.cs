using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZealandDimselab.DTO
{
    public class BookingItemDto
    {
        [JsonPropertyName("bookingId")]

        public int BookingId { get; set; }
        [JsonPropertyName("booking")]

        public BookingDto Booking { get; set; }
        [JsonPropertyName("itemId")]

        public int ItemId { get; set; }
        [JsonPropertyName("item")]

        public ItemDto Item { get; set; }
        [JsonPropertyName("quantity")]

        public int Quantity { get; set; }
    }
}
