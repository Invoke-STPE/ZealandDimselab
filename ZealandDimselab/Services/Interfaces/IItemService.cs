using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services.Interfaces
{
    public interface IItemService
    {
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<Item>> GetAllItems();
        Task<List<Item>> GetAllItemsWithCategoriesAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<List<Item>> GetItemsWithCategoryIdAsync(int id);
        Task<Item> GetItemWithCategoriesAsync(int id);
        Task ItemStockUpdateAsync(Item item, int bookedQuantity);
        Task UpdateItemAsync(int id, Item item);
    }
}