using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Categories
{
    public class AllItemsCategoryModel : PageModel
    {
        public List<Item> Items { get; set; }
        private ItemService _itemService;

        public AllItemsCategoryModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task OnGetAsync(int id)
        {
            Items = await _itemService.GetItemsWithCategoryIdAsync(id);
        }
    }
}