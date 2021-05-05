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
    public class DeleteItemModel : PageModel
    {
        private ItemService itemService;
        public Item Item { get; set; }
        public List<Item> Items { get; set; }

        public DeleteItemModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await itemService.GetItemWithCategoriesAsync(id);
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await itemService.DeleteItemAsync(id);
            return RedirectToPage("/Items/AllItems");
        }
    
    }
}
