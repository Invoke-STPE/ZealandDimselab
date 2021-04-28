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
        private ItemService ItemService { get; set; }
        public List<Item> Items;
        
        [BindProperty]
        public Item Item { get; set; }

        public ItemDetailsModel(ItemService itemService)
        {
            ItemService = itemService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await ItemService.GetItemByIdAsync(id);
            return Page();
        }
    }
}
