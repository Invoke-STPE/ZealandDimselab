using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public interface IHttpClientItem
    {
        Task<List<ItemDto>> GetAllItemsWithCategoriesAsync();
        Task<ItemDto> GetItemByIdAsync(int id);
        Task<List<ItemDto>> GetItemsWithCategoryIdAsync(int categoryId);
        Task AddItemAsync(ItemDto item);
        Task<List<ItemDto>> GetAllItemsAsync();
        Task UpdateItemAsync(ItemDto item);
        Task ItemStockUpdateAsync(ItemDto item, int bookingQuantity);
        Task<ItemDto> GetItemWithCategoriesAsync(int id);
        Task DeleteItemAsync(int id);
    }
}