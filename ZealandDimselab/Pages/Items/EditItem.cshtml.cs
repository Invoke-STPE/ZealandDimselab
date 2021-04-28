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
    public class EditItemModel : PageModel
    {
        private ItemService ItemService { get; set; }
        [BindProperty]
        public Item Item { get; set; }

        public EditItemModel(ItemService itemService)
        {
            this.ItemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await ItemService.GetItemByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Item item)
        {
            await ItemService.UpdateItemAsync(item.Id, item);
            return RedirectToPage("/Items/AllItems");
        }
    }
}
