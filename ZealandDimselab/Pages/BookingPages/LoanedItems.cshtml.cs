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
    public class LoanedItemsModel : PageModel
    {
        private BookingService _bookingService;
        public List<BookedItem> BookedItems;

        public LoanedItemsModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }


        public async Task OnGetAsync()
        {
            BookedItems = await _bookingService.GetAllBookedItemsAsync();
        }
    }
}
