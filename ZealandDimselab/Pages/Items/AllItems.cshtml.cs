using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Helpers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.DTO;

namespace ZealandDimselab.Pages.Items
{
    public class AllItemsModel : PageModel
    {
        private readonly IHttpClientItem _httpClient;

        public List<ItemDto> Items { get; set; }
        public int CategoryId { get; set; }
        public List<ItemDto> Cart { get; set; }

        public AllItemsModel(IHttpClientItem httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await _httpClient.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int category)
        {
            if (category == 0) return OnGetAsync().Result;
            CategoryId = category;
            Items = await _httpClient.GetItemsWithCategoryIdAsync(category);
            return Page();
        }

        public async Task<IActionResult> OnGetAddToCart(int id)
        {
            Cart = SessionHelper.GetObjectFromJson<List<ItemDto>>(HttpContext.Session, "cart");

            if (Cart == null) // Check if Cart exists in user cache.
            {
                Cart = new List<ItemDto>
                {
                    await _httpClient.GetItemByIdAsync(id)
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            else // If it does, append to that.
            {
                int index = SessionHelper.Exists(Cart, id);
                if (index == -1) // if the item does not exists in the cart, append it.
                {
                    Cart.Add(await _httpClient.GetItemByIdAsync(id));
                }
                //else 
                //{
                //    Cart[index].Quantity++;
                //}
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            return RedirectToPage();
        }
    }
}
