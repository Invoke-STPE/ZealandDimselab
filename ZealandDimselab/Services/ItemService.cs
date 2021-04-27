using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZealandDimselab.MockData;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;

namespace ZealandDimselab.Services
{
    public class ItemService
    {
        private List<Item> _items;
        public GenericDbService<Item> DbService { get; set; }

        public ItemService(GenericDbService<Item> dbService)
        {
            DbService = dbService;
            _items = dbService.GetObjectsAsync().Result.ToList();
        }

        public async Task AddItemAsync(Item item)
        {
            _items.Add(item);
            await DbService.AddObjectAsync(item);
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await DbService.GetObjectByIdAsync(id);
        }

        public List<Item> GetAllItems()
        {
            return _items;
        }

        public async Task DeleteItemAsync(int id)
        {
            Item item = await DbService.GetObjectByIdAsync(id);

            if (item != null)
            {
                await DbService.DeleteObjectAsync(item);
                _items = (await DbService.GetObjectsAsync()).ToList();
            }
        }

        public async Task UpdateItemAsync(int id, Item item)
        {
            if (item != null)
            {
                item.Id = id;
                await DbService.UpdateObjectAsync(item);
                _items = (await DbService.GetObjectsAsync()).ToList();
            }
        }

        public IEnumerable<Item> FilterByName(string name)
        {
            return from item in _items
                   where item.Name == name
                   select item;
        }
    }
}