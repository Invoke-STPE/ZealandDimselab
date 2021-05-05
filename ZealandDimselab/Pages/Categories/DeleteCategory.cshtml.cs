using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        private ItemService itemService;
        private CategoryService categoryService;
        public Category Category { get; set; }
        public List<Item> Items { get; set; }
        public List<Category> Categories { get; set; }

        public DeleteCategoryModel(ItemService itemService, CategoryService categoryService)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await categoryService.GetCategoryByIdAsync(id);
            Categories = categoryService.GetAllCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToPage("/Index");
        }
    }
}
