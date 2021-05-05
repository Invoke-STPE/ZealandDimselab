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
    public class ItemDetailsModel : PageModel
    {
        private ItemService itemService;
        public Item Item { get; set; }
        public List<Item> Items { get; set; }

        public ItemDetailsModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task OnGetAsync(int id)
        {
            Item = await itemService.GetItemWithCategoriesAsync(id);
            Items = await itemService.GetAllItemsWithCategoriesAsync();
        }
    }
}
