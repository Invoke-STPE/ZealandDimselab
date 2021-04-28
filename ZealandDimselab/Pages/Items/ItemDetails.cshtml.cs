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
        public Item Item;
        public List<Item> Items;

        public ItemDetailsModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task OnGetAsync(int id)
        {
            Item = await itemService.GetItemByIdAsync(id);
            Items = itemService.GetAllItems();
        }
    }
}
