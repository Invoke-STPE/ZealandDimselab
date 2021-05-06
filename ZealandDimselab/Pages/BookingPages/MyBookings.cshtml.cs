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

        public MyBookingsModel(/*BookingService bookingService*/)
        {
            //this.bookingService = bookingService;
        }
        public IActionResult OnGet()
        {
            
            if (HttpContext.User.IsInRole("admin"))
            {
                //Bookings = bookingService.GetAllBookings();
                Bookings = null;
            } else
            {
                Bookings = MockData.MockDataBooking.GetBookings();
                // Bookings = bookingService.GetBookingsByEmail(HttpContext.User.Identity.Name);
            }

            return Page();

        }
    }
}
