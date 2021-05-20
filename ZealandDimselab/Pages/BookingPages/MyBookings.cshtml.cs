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
        public async Task<IActionResult> OnGetAsync()
        {

                Bookings = (await bookingService.GetAllBookingsAsync()).ToList();

            return Page();

        }
    }
}
