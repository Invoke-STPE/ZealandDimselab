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
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        [BindProperty] 
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

        public CreateCategoryModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await GetAllCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await GetAllCategoriesAsync();
                return Page();
            }

            await AddCategoryAsync(Category);
            return RedirectToPage("/Index");
        }

        private async Task<List<Category>> GetAllCategoriesAsync()
        {
            string url = _configuration.GetValue<string>("CategoryAPI:BaseUrlCategory");
            var response = await _httpClient.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Category>>(jsonResult);
        }

        private async Task AddCategoryAsync(Category category)
        {
            string url = _configuration.GetValue<string>("CategoryAPI:BaseUrlCategory");
            string jsonString = JsonSerializer.Serialize(category);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(url, stringContent);
        }
    }
}
