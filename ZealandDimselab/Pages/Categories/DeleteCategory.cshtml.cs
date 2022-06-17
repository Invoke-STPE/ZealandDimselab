using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class DeleteCategoryModel : PageModel
    {

        private readonly IHttpClientCategory _httpClientCategory;

        public CategoryDto Category { get; set; }
        public List<CategoryDto> Categories { get; set; }

        public DeleteCategoryModel(IHttpClientCategory httpClientCategory)
        {
            _httpClientCategory = httpClientCategory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _httpClientCategory.GetCategoryByIdAsync(id);
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _httpClientCategory.DeleteCategoryAsync(id);
            return RedirectToPage("/Index");
        }


    }
}
