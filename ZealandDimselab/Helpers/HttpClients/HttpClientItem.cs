using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public class HttpClientItem : IHttpClientItem
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlBase = "ItemAPI:BaseUrlItem";

        public HttpClientItem(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task AddItemAsync(ItemDto item)
        {
            string jsonString = JsonSerializer.Serialize(item);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, _urlBase);

            await _httpClient.PostAsync(jsonString, stringContent);

        }

        public async Task DeleteItemAsync(int id)
        {
            string url = _configuration.GetValue<string>(_urlBase);
            await _httpClient.DeleteAsync($"{url}/{id}");
        }

        public async Task<List<ItemDto>> GetAllItemsAsync()
        {
            string url = _configuration.GetValue<string>(_urlBase);
            var response = await _httpClient.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<List<ItemDto>>(response); ;
        }

        public async Task<List<ItemDto>> GetAllItemsWithCategoriesAsync()
        {
            string url = _configuration.GetValue<string>("ItemAPI:GetAllItemsWithCategories");
            var response = await _httpClient.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ItemDto>>(jsonResult); ;
        }
        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            string urlBase = _configuration.GetValue<string>(_urlBase);
            string url = $"{urlBase}/{id}";
            var response = await _httpClient.GetStreamAsync(url);

            return await JsonSerializer.DeserializeAsync<ItemDto>(response); ;
        }

        public async Task<List<ItemDto>> GetItemsWithCategoryIdAsync(int categoryId)
        {
            var builder = new UriBuilder(_configuration.GetValue<string>("ItemAPI:GetItemsWithCategoryId"))
            {
                Query = $"id={categoryId}"
            };
            string url = builder.ToString();

            var response = await _httpClient.GetStreamAsync(url);


            return await JsonSerializer.DeserializeAsync<List<ItemDto>>(response);
        }

        public async Task<ItemDto> GetItemWithCategoriesAsync(int id)
        {
            var builder  = new UriBuilder(_configuration.GetValue<string>("ItemAPI:GetItemWithCategories"))
            {
                Query = $"id={id}"
            };

            string url = builder.ToString();

            var response = await _httpClient.GetStreamAsync(url);


            return await JsonSerializer.DeserializeAsync<ItemDto>(response);
        }

        public async Task ItemStockUpdateAsync(ItemDto item, int bookingQuantity)
        {
            item.Stock = item.Stock - bookingQuantity;
            string itemInJson = JsonSerializer.Serialize(item);
            StringContent stringContent = new StringContent(itemInJson, Encoding.UTF8, "application/json");
            string url = _configuration.GetValue<string>("ItemAPI:BaseUrlItem");
            await _httpClient.PutAsync(url, stringContent);
        }

        public async Task UpdateItemAsync(ItemDto item)
        {
            string jsonString = JsonSerializer.Serialize(item);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, _urlBase);

            await _httpClient.PutAsync(jsonString, stringContent);
        }
    }
}
