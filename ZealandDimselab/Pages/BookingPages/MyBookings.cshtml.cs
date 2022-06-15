using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.BookingPages
{
    public class MyBookingsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public List<Booking> Bookings { get; set; }

        public MyBookingsModel(HttpClient httpClient, IConfiguration configuration )
        {
            Bookings = new List<Booking>();
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("teacher"))
            {
                Bookings = (await GetAllBookingsAsync()).ToList();
            } else if (HttpContext.User.IsInRole("student"))
            {
                Bookings = (await GetBookingsByEmailAsync(HttpContext.User.Identity.Name)).ToList();
            }
         

            return Page();

        }

        // Confirms that a booking have been returned.
        public async Task<IActionResult> OnGetConfirmReturnAsync(int id)
        {
            if (HttpContext.User.IsInRole("admin"))
            {
                await ReturnedBooking(id); // Need to implement this
            }
            return RedirectToPage("MyBookings");
        }


        private async Task<List<Booking>> GetAllBookingsAsync()
        {
            string url = _configuration.GetValue<string>("BookingAPI:BaseUrlBooking");

            var response = await _httpClient.GetAsync(url);

            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Booking>>(jsonResult);
        }

        private async Task<List<Booking>> GetBookingsByEmailAsync(string email)
        {
            var builder = new UriBuilder(_configuration.GetValue<string>("BookingAPI:GetBookingsByEmail"));
            builder.Query = $"email={email}";

            var response = await _httpClient.GetAsync(builder.ToString());

            string jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Booking>>(jsonString);
        }

        private async Task ReturnedBooking(int id)
        {
        }
    }
}
