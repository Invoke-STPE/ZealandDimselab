using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
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

        public async Task AddBookingAsync(BookingDto booking)
        {
            string bookingInJson = JsonSerializer.Serialize(booking);
            StringContent stringContent = new StringContent(bookingInJson, Encoding.UTF8, "application/json");
            string url = _configuration.GetValue<string>(_baseUrl);
            await _httpClient.PostAsync(url, stringContent);
        }

        public async Task<List<BookedItemDto>> GetAllBookedItemsAsync()
        {
            var url = _configuration.GetValue<string>(_baseUrl);
            var response = await _httpClient.GetAsync(url);

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BookedItemDto>>(jsonResult);
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync()
        {
            string url = _configuration.GetValue<string>(_baseUrl);

            var response = await _httpClient.GetAsync(url);

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<BookingDto>>(jsonResult);
        }

        public async Task<List<BookingDto>> GetBookingsByEmailAsync(string email)
        {
            string url = _configuration.GetValue<string>("BookingAPI:GetBookingsByEmail");
            var builder = new UriBuilder(url)
            {
                Query = $"email={email}"
            };
            var response = await _httpClient.GetStreamAsync(builder.ToString());
            return await JsonSerializer.DeserializeAsync<List<BookingDto>>(response);
        }

        //TODO: need to implement this
        public async Task ReturnedBooking(int id)
        {
        }
    }
}
