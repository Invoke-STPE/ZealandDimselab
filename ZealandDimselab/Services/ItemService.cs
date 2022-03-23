using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ZealandDimselab.Services.Interfaces;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _itemDbService;
        public ItemService(IItemRepository itemDbService)
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
            item.Stock = item.Quantity;
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

        public async Task ItemStockUpdateAsync(Item item, int bookedQuantity)
        {
            item.Stock = item.Stock - bookedQuantity;
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