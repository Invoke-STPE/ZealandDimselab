using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Items
{
    public class ItemDetailsModel : PageModel
    {
        private readonly IHttpClientItem _httpClientItem;

        public ItemDto Item { get; set; }
        public List<ItemDto> Items { get; set; }
        public int CategoryId { get; set; }

        public ItemDetailsModel(IHttpClientItem httpClientItem)
        {
            _httpClientItem = httpClientItem;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            Items = await _httpClientItem.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategory(int id, int category)
        {
            if (category == 0) return RedirectToPage("ItemDetails", new { id });
            Items = await _httpClientItem.GetItemsWithCategoryIdAsync(category);
            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            CategoryId = category;
            return Page();
        }
    }
}
