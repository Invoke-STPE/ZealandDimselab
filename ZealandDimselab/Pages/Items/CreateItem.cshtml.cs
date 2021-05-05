using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class CreateItemModel : PageModel
    {
        private ItemService itemService;
        private CategoryService categoryService;
        [BindProperty]
        public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }

        public CreateItemModel(ItemService itemService, CategoryService categoryService)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            Categories = categoryService.GetAllCategories();
            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync();
            }

            await itemService.AddItemAsync(Item);
            return RedirectToPage("AllItems");
        }
    }
}
