using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;
using System.IO;
using ZealandDimselab.DTO;

namespace ZealandDimselab.Helpers.HttpClients
{
    public class HttpClientCategory : IHttpClientCategory
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "CategoryAPI:BaseUrlCategory";

        public HttpClientCategory(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            string url = _configuration.GetValue<string>(_baseUrl);
            var response = await _httpClient.GetStreamAsync(url);
            List<CategoryDto> categories = await JsonSerializer.DeserializeAsync<List<CategoryDto>>(response);

            return categories;
        }

        public async Task AddCategoryAsync(CategoryDto category)
        {
            string url = _configuration.GetValue<string>(_baseUrl);
            string jsonString = JsonSerializer.Serialize(category);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(url, stringContent);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            string url = _configuration.GetValue<string>(_baseUrl);
            var response = await _httpClient.GetStreamAsync($"{url}/{id}");
            return await JsonSerializer.DeserializeAsync<CategoryDto>(response);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            string url = _configuration.GetValue<string>(_baseUrl);
            await _httpClient.DeleteAsync($"{url}/{id}");
        }

        public async Task UpdateCategoryAsync(CategoryDto category)
        {
            string url = _configuration.GetValue<string>(_baseUrl);
            string jsonObject = JsonSerializer.Serialize(category);
            StringContent stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(url, stringContent);
        }
    }
}
