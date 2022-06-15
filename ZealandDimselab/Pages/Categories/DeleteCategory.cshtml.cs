using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string urlBase = "CategoryAPI:BaseUrlCategory";

        public Category Category { get; set; }
        public List<Category> Categories { get; set; }

        public DeleteCategoryModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await GetCategoryByIdAsync(id);
            Categories = await GetAllCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await DeleteCategoryAsync(id);
            return RedirectToPage("/Index");
        }

        private async Task<List<Category>> GetAllCategoriesAsync()
        {
            string url = _configuration.GetValue<string>(urlBase);
            var response = await _httpClient.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Category>>(jsonResult);
        }

        private async Task<Category> GetCategoryByIdAsync(int id)
        {
            string url = _configuration.GetValue<string>(urlBase);
            var response = await _httpClient.GetAsync($"{url}/{id}");
            string jsonResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(jsonResult);
        }

        private async Task DeleteCategoryAsync(int id)
        {
            string url = _configuration.GetValue<string>(urlBase);
            await _httpClient.DeleteAsync($"{url}/{id}");

        }
    }
}
