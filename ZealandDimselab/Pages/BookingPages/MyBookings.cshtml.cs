using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.BookingPages
{
    public class MyBookingsModel : PageModel
    {
        
        private readonly IHttpClientBooking _httpClientBooking;

        public List<BookingDto> Bookings { get; set; }

        public MyBookingsModel(IHttpClientBooking httpClientBooking)
        {
            Bookings = new List<BookingDto>();
        
            _httpClientBooking = httpClientBooking;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("teacher"))
            {
                Bookings = (await _httpClientBooking.GetAllBookingsAsync()).ToList();
            } else if (HttpContext.User.IsInRole("student"))
            {
                Bookings = (await _httpClientBooking.GetBookingsByEmailAsync(HttpContext.User.Identity.Name)).ToList();
            }
            return Page();

        }

        // Confirms that a booking have been returned.
        public async Task<IActionResult> OnGetConfirmReturnAsync(int id)
        {
            if (HttpContext.User.IsInRole("admin"))
            {
                await _httpClientBooking.ReturnedBooking(id); // Need to implement this
            }
            return RedirectToPage("MyBookings");
        }


       

   
    }
}
