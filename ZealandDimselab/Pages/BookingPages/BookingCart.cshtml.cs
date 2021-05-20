using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Helpers;
using ZealandDimselab.Models;
using ZealandDimselab.MockData;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.BookingPages
{
    public class BookingCartModel : PageModel
    {
        public List<Item> Cart { get; set; }
        public double Total { get; set; }
        private readonly UserService userService;
        private readonly ItemService itemService;
        private readonly BookingService bookingService;

        [BindProperty]
        public Booking Booking { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public BookingCartModel(UserService userService, BookingService bookingService, ItemService itemService)
        {
            this.userService = userService;
            this.itemService = itemService;
            this.bookingService = bookingService;
        }

        public void OnGet()
        {
            Cart = GetCart();
            if (Cart == null)
            {
                Cart = new List<Item>();
            }
        }

        /// <summary>
        /// When redirect to this OnGet, it will create or update an cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAddToCart(int id)
        {
            Cart = GetCart();

            if (Cart == null) // Check if Cart exists in user cache.
            {
                Cart = new List<Item>
                {
                    await itemService.GetItemByIdAsync(id)
                };
                SetCart(Cart);
            }
            else // If it does, append to that.
            {
                int index = Exists(Cart, id);
                if (index == -1) // if the item does not exists in the cart, append it.
                {
                    Cart.Add( await itemService.GetItemByIdAsync(id) );
                }
                //else 
                //{
                //    Cart[index].Quantity++;
                //}
                SetCart(Cart);
            }
            return Page();
        }

        /// <summary>
        /// Deletes the item from the cart storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnPostDelete(int id)
        {
            Cart = GetCart();
            int index = Exists(Cart, id);
            Cart.RemoveAt(index);
            SetCart(Cart);
            return RedirectToPage("BookingCart");
        }

        public async Task<IActionResult> OnPostCreate()
        {
            Cart = GetCart();
            User user = await userService.GetUserByEmail(Email);
            if (user != null)
            {
                Booking booking = CreateBooking(Email, user);
                await bookingService.AddBookingAsync(booking);
            } else
            {
                await userService.AddUserAsync(new User() { Email = Email });
                user = await userService.GetUserByEmail(Email);
                Booking booking = CreateBooking(Email, user);
                await bookingService.AddBookingAsync(booking);
            }
            return RedirectToPage("MyBookings");
        }

        public IActionResult OnPostClearCart()
        {
            SetCart(new List<Item>());
            return RedirectToPage("BookingCart");
        }

        /// <summary>
        /// Check if an item in a cart exists.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns>Returns index of item, if none is found it returns -1</returns>
        private int Exists(List<Item> cart, int id)
        {
            for (int i = 0; i < Cart.Count; i++)
            {
                if (Cart[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Gets the current session's Cart.
        /// </summary>
        /// <returns></returns>
        private List<Item> GetCart()
        {
            return SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
        }
        private void SetCart(List<Item> cart)
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        }

        private Booking CreateBooking(string email, User user)
        {
            Cart = GetCart();
            Booking.BookingDate = DateTime.Now.Date;
            Booking.UserId = user.Id;
            
            foreach (var item in Cart)
            {
                Booking.BookingItems.Add(new BookingItem { ItemId = item.Id });
            }
            return Booking;
        }
    }
}
