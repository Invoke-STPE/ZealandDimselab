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
    public class HttpItemService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "";

        public HttpItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = ConfigurationManager.AppSettings["ItemBaseUrl"];
        }

        public async Task AddItemAsync(ItemDto item)
        {
            string jsonString = JsonSerializer.Serialize(item);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_baseUrl, stringContent);

        }

        public async Task DeleteItemAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }

        public async Task<List<ItemDto>> GetAllItemsAsync()
        {
            var response = await _httpClient.GetStreamAsync(_baseUrl);
            return await JsonSerializer.DeserializeAsync<List<ItemDto>>(response); ;
        }

        public async Task<List<ItemDto>> GetAllItemsWithCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            string jsonResult = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ItemDto>>(jsonResult); ;
        }
        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            string url = $"{_baseUrl}/{id}";
            var response = await _httpClient.GetStreamAsync(url);

            return await JsonSerializer.DeserializeAsync<ItemDto>(response); ;
        }

        public async Task<List<ItemDto>> GetItemsWithCategoryIdAsync(int categoryId)
        {
            var builder = new UriBuilder($"{_baseUrl}/GetItemsWithCategoryId")
            {
                Query = $"id={categoryId}"
            };
            string url = builder.ToString();

            var response = await _httpClient.GetStreamAsync(url);


            return await JsonSerializer.DeserializeAsync<List<ItemDto>>(response);
        }

        public async Task<ItemDto> GetItemWithCategoriesAsync(int id)
        {
            var builder  = new UriBuilder($"{ _baseUrl }/GetItemWithCategories")
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
            await _httpClient.PutAsync(_baseUrl, stringContent);
        }

        public async Task UpdateItemAsync(ItemDto item)
        {

            string jsonString = JsonSerializer.Serialize(item);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(_baseUrl, stringContent);
        }
    }
}
