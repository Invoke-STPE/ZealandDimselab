using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class AllItemsModel : PageModel
    {
        public List<Item> Items { get; set; }
        private readonly ItemService _itemService;
        public int CategoryId { get; set; }

        public AllItemsModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await _itemService.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int category)
        {
            if (category == 0) return OnGetAsync().Result;
            CategoryId = category;
            Items = await _itemService.GetItemsWithCategoryIdAsync(category);
            return Page();
        }
    }
}
