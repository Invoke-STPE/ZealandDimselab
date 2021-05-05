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
        private CategoryService categoryService;
        [BindProperty]
        public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }

        public EditItemModel(ItemService itemService, CategoryService categoryService)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            Categories = categoryService.GetAllCategories();
            Item = await itemService.GetItemWithCategoriesAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Items = await itemService.GetAllItemsWithCategoriesAsync();
                Categories = categoryService.GetAllCategories();
                Item = await itemService.GetItemWithCategoriesAsync(Item.Id);
                return Page();
            }

            await itemService.UpdateItemAsync(Item.Id, Item);
            return RedirectToPage("/Items/AllItems");
        }
    }
}
