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
        [BindProperty] public Item Item { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty] public int CategoryId { get; set; }

        public EditItemModel(ItemService itemService, CategoryService categoryService)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Items = await itemService.GetAllItemsWithCategoriesAsync();
            Categories = await categoryService.GetAllCategoriesAsync();
            Item = await itemService.GetItemWithCategoriesAsync(id);
            CategoryId = 0;

            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int id, int category)
        {
            if (category == 0) return RedirectToPage("EditItem", new { id = id });

            Items = await itemService.GetItemsWithCategoryIdAsync(category);
            Categories = await categoryService.GetAllCategoriesAsync();
            Item = await itemService.GetItemWithCategoriesAsync(id);
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (CategoryId == 0) return await OnGetAsync(Item.Id);
                return await OnGetFilterByCategoryAsync(Item.Id, CategoryId);
            }

            await itemService.UpdateItemAsync(Item.Id, Item);
            return RedirectToPage("/Items/AllItems", "FilterByCategory", new { category = CategoryId });
        }
    }
}
