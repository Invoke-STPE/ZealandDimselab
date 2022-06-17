using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZealandDimselab.DTO
{
    public class BookingDto
    {
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("bookingItems")]

        public List<BookingItemDto> BookingItems { get; set; }
        [JsonPropertyName("userId")]

        public int UserId { get; set; }
        [JsonPropertyName("user")]

        public UserDto User { get; set; }
        [JsonPropertyName("detail")]

        public string Details { get; set; }
        [JsonPropertyName("bookingDate")]

        public DateTime BookingDate { get; set; }
        [JsonPropertyName("returnDate")]

        public DateTime ReturnDate { get; set; }
        [JsonPropertyName("returned")]

        public bool Returned { get; set; }
    }
}