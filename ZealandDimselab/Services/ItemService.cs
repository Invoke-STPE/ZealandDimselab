using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class ItemService: GenericService<Item>
    {
        private ItemDbService _itemDbService;
        public ItemService(IDbService<Item> dbService, ItemDbService itemDbService) : base(dbService)
        {
            _itemDbService = itemDbService;
        }

        public List<Item> GetAllItems()
        {
            return GetAllObjects();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await GetObjectByKeyAsync(id);
        }

        public async Task AddItemAsync(Item item)
        {
            await AddObjectAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await DeleteObjectAsync(await GetItemByIdAsync(id));
        }

        public async Task UpdateItemAsync(int id, Item item)
        {
            item.Id = id;
            await UpdateObjectAsync(item);
        }
        
        public IEnumerable<Item> FilterByName(string name)
        {
            return from item in GetAllItems()
                   where item.Name == name
                   select item;
        }

        //public async Task<List<Item>> GetAllItemsWithCategoriesAsync()
        //{
        //    return await _itemDbService.GetAllItemsWithCategoriesAsync();
        //}

        //public async Task<Item> GetItemWithCategoriesAsync(int id)
        //{
        //    return await _itemDbService.GetItemWithCategoriesAsync(id);
        //}

        //public async Task AddItemCategory(Item item, Category category)
        //{
        //    item.Categories.Add(category);
        //    await UpdateItemAsync(item.Id, item);
        //}
    }
}