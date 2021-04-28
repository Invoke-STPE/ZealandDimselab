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
        private ItemService _itemService;
        [BindProperty]
        public Item Item { get; set; }

        public CreateItemModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _itemService.AddItemAsync(Item);
            return RedirectToPage("AllItems");
        }
    }
}
