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
    public class CreateItemModel : PageModel
    {
        private ItemService itemService;
        [BindProperty]
        public Item Item { get; set; }
        public List<Item> Items { get; set; }

        public CreateItemModel(ItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult OnGet()
        {
            Items = itemService.GetAllItems();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Items = itemService.GetAllItems();
                return Page();
            }

            await itemService.AddItemAsync(Item);
            return RedirectToPage("AllItems");
        }
    }
}
