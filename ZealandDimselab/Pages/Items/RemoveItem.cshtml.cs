using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.RemoveItem
{
    public class RemoveItemModel : PageModel
    {
        private ItemService ItemService { get; set; }
        public Item Item { get; set; }

        public RemoveItemModel(ItemService itemService)
        {
            this.ItemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await ItemService.GetItemByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await ItemService.DeleteItemAsync(id);
            return RedirectToPage("/Items/AllItems");
        }
    
    }
}
