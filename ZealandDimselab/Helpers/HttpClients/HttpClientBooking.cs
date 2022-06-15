using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public class HttpClientBooking : IHttpClientBooking
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl = "BookingAPI:BaseUrlBooking";

        public HttpClientBooking(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            string bookingInJson = JsonSerializer.Serialize(booking);
            StringContent stringContent = new StringContent(bookingInJson, Encoding.UTF8, "application/json");
            string url = _configuration.GetValue<string>(_baseUrl);
            await _httpClient.PostAsync(url, stringContent);
        }

        public async Task<List<BookedItem>> GetAllBookedItemsAsync()
        {
            var url = _configuration.GetValue<string>(_baseUrl);
            var response = await _httpClient.GetAsync(url);

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BookedItem>>(jsonResult);
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            string url = _configuration.GetValue<string>(_baseUrl);

            var response = await _httpClient.GetAsync(url);

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Booking>>(jsonResult);
        }

        public async Task<List<Booking>> GetBookingsByEmailAsync(string email)
        {
            var builder = new UriBuilder(_configuration.GetValue<string>(_baseUrl));
            builder.Query = $"email={email}";

            var response = await _httpClient.GetAsync(builder.ToString());

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Booking>>(jsonString);
        }

        //TODO: need to implement this
        public async Task ReturnedBooking(int id)
        {
        }
    }
}
