using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class ItemService
    {
        private ItemDbService _itemDbService;
        public ItemService(IDbService<Item> dbService, ItemDbService itemDbService)
        {
            _itemDbService = itemDbService;
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _itemDbService.GetObjectsAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _itemDbService.GetObjectByKeyAsync(id);
        }

        public async Task AddItemAsync(Item item)
        {
            await _itemDbService.AddObjectAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _itemDbService.DeleteObjectAsync(await GetItemByIdAsync(id));
        }

        public async Task UpdateItemAsync(int id, Item item)
        {
            item.Id = id;
            await _itemDbService.UpdateObjectAsync(item);
        }
        
        //public IEnumerable<Item> FilterByName(string name)
        //{
        //    return from item in GetAllItems()
        //           where item.Name == name
        //           select item;
        //}

        public async Task<List<Item>> GetAllItemsWithCategoriesAsync()
        {
            return await _itemDbService.GetAllItemsWithCategoriesAsync();
        }

        public async Task<Item> GetItemWithCategoriesAsync(int id)
        {
            return await _itemDbService.GetItemWithCategoriesAsync(id);
        }

        public async Task<List<Item>> GetItemsWithCategoryIdAsync(int id)
        {
            return await _itemDbService.GetItemsWithCategoryId(id);
        }
    }
}