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
        private ItemService itemService;
        [BindProperty]
        public Item Item { get; set; }
        public List<Item> Items { get; set; }

        public EditItemModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Items = itemService.GetAllItems();
            Item = await itemService.GetItemByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Items = itemService.GetAllItems();
                Item = await itemService.GetItemByIdAsync(Item.Id);
                return Page();
            }

            await itemService.UpdateItemAsync(Item.Id, Item);
            return RedirectToPage("/Items/AllItems");
        }
    }
}
