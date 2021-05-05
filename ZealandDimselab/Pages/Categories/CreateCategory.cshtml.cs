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
    public class CreateCategoryModel : PageModel
    {
        private CategoryService categoryService;
        private ItemService itemService;
        [BindProperty] 
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public List<Item> Items { get; set; }

        public CreateCategoryModel(CategoryService categoryService, ItemService itemService)
        {
            this.categoryService = categoryService;
            this.itemService = itemService;
        }

        public IActionResult OnGet()
        {
            Categories = categoryService.GetAllCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = categoryService.GetAllCategories();
                return Page();
            }

            await categoryService.AddCategoryAsync(Category);
            return RedirectToPage("/Index");
        }
    }
}
