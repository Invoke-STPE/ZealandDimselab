using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;


namespace ZealandDimselab.Pages.BookingPages
{
    public class LoanedItemsModel : PageModel
    {
        public List<BookedItemDto> BookedItems;

        private readonly IHttpClientBooking _httpClientBooking;

        public LoanedItemsModel(IHttpClientBooking httpClientBooking)
        {
            _httpClientBooking = httpClientBooking;
        }


        public async Task OnGetAsync()
        {
            BookedItems = await _httpClientBooking.GetAllBookedItemsAsync();
        }

        public async Task OnGetSortByIdAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Item.Id).ToList();
        }

        public async Task OnGetSortByIdDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Item.Id).ToList();
        }

        public async Task OnGetSortByItemNameAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Item.Name).ToList();
        }

        public async Task OnGetSortByItemNameDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Item.Name).ToList();
        }

        public async Task OnGetSortByQuantityAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Quantity).ToList();
        }

        public async Task OnGetSortByQuantityDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Quantity).ToList();
        }

        public async Task OnGetSortByBookingIdAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.BookingId).ToList();
        }

        public async Task OnGetSortByBookingIdDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.BookingId).ToList();
        }

        public async Task OnGetSortByUserNameAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.User.Email).ToList();
        }

        public async Task OnGetSortByUserNameDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.User.Email).ToList();
        }

        public async Task OnGetSortByBookingDateAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.BookingDate).ToList();
        }

        public async Task OnGetSortByBookingDateDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.BookingDate).ToList();
        }

        public async Task OnGetSortByReturnDateAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.ReturnDate).ToList();
        }

        public async Task OnGetSortByReturnDateDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.ReturnDate).ToList();
        }
        public async Task OnGetSortByStatusAscendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Status).ToList();
        }

        public async Task OnGetSortByStatusDescendingAsync()
        {
            List<BookedItemDto> unsortedList = (await _httpClientBooking.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Status).ToList();
        }

        //public async Task OnGetFilterByItemId(int id)
        //{

        //}

        //public async Task OnGetFilterByQuantity(int min, int max)
        //{

        //}

        //public async Task OnGetFilterByItemNameAsync(string name)
        //{

        //}

        //public async Task OnGetFilterByBookedByAsync(string name)
        //{

        //}

        //public async Task OnGetFilterByStatus(string status)
        //{

        //}

        
    }
}
