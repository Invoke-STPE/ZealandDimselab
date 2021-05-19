using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.BookingPages
{
    public class MyBookingsModel : PageModel
    {
        private readonly BookingService bookingService;

        public List<Booking> Bookings { get; set; }

        public MyBookingsModel(BookingService bookingService)
        {
            this.bookingService = bookingService;
        }
        public IActionResult OnGetAsync()
        {
            
            if (HttpContext.User.IsInRole("admin"))
            {
                Bookings = bookingService.GetAllBookingsAsync().Result.ToList();
                //Bookings = null;
            } else
            {
                //Bookings = MockData.MockDataBooking.GetBookings();
                Bookings = bookingService.GetBookingsByEmailAsync(HttpContext.User.Identity.Name).Result;
            }

            return Page();

        }
    }
}
