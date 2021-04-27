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
        private GenericDbService<Item> _dbService;

        private List<Item> _items;
        [BindProperty]
        public Item Item { get; set; }

        public CreateItemModel(ItemService itemService, GenericDbService<Item> dbService)
        {
            _dbService = dbService;
            _itemService = itemService;

            _items = (List<Item>)dbService.GetObjectsAsync().Result;
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
