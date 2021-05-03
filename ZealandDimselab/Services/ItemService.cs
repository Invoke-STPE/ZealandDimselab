using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class ItemService: GenericService<Item>
    {
        private CategoryService _categoryService;

        public ItemService(IDbService<Item> dbService, CategoryService categoryService) : base(dbService)
        {
            _categoryService = categoryService;
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

        public async Task AddItemCategory(Item item, Category category)
        {
            item.Category = category;
            await UpdateItemAsync(item.Id, item);
        }
    }
}