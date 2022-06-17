using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {

        private readonly IHttpClientCategory _httpClientCategory;

        [BindProperty]
        public CategoryDto Category { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public EditCategoryModel(IHttpClientCategory httpClientCategory)
        {
            _httpClientCategory = httpClientCategory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            Category = await _httpClientCategory.GetCategoryByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Categories = await _httpClientCategory.GetAllCategoriesAsync();
                Category = await _httpClientCategory.GetCategoryByIdAsync(id);
                return Page();
            }

            await _httpClientCategory.UpdateCategoryAsync(Category);
            return RedirectToPage("/Index");

        }

        
    }
}
