using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.WPF.DtoModels;

namespace ZealandDimselab.WPF.Services
{
    public class HttpCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "";

        public HttpCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = ConfigurationManager.AppSettings["CategoryBaseUrl"];
        }
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetStreamAsync(_baseUrl);
            List<CategoryDto> categories = await JsonSerializer.DeserializeAsync<List<CategoryDto>>(response);
            return categories;
        }

        public async Task AddCategoryAsync(CategoryDto category)
        {
            string jsonString = JsonSerializer.Serialize(category);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_baseUrl, stringContent);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var response = await _httpClient.GetStreamAsync($"{_baseUrl}/{id}");
            return await JsonSerializer.DeserializeAsync<CategoryDto>(response);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task UpdateCategoryAsync(CategoryDto category)
        {
            string jsonObject = JsonSerializer.Serialize(category);
            StringContent stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(_baseUrl, stringContent);
        }
    }
}
