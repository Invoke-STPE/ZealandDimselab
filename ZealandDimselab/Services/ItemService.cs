using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.MockData;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;

namespace ZealandDimselab.Services
{
    public class ItemService
    {
        private Dictionary<int, Item> items;

        private IRepository<Item> repository;

        public ItemService(IRepository<Item> repository)
        {
            this.repository = repository;
            items = repository.GetAllAsync().ToDictionary(i => i.Id);
        }

        public async Task AddItemAsync(Item item)
        {
            items.Add(item.Id, item);
            await repository.AddObjectAsync(item);
        }

        public Item GetItemById(int id)
        {
            Item item = items[id];
            return item;
        }

        public List<Item> GetAllItems()
        {
            return items.Values.ToList();
        }

        public async Task<Item> DeleteItemAsync(int id)
        {
            Task<Item> itemToBeDeleted = repository.GetObjectByIdAsync(id);
            
            if (itemToBeDeleted != null)
            {
                items.Remove(id);
                await repository.DeleteObjectAsync(itemToBeDeleted.Result);
            }

            return itemToBeDeleted.Result;
        }

        public async Task UpdateItemAsync(Item item)
        {
            if (item != null)
            {
                await repository.UpdateObjectAsync(item);
                items = repository.GetAllAsync().ToDictionary(i => i.Id);
            }
        }

        public IEnumerable<Item> FilterByName(string name)
        {
            return from item in items.Values
                   where item.Name == name
                   select item;
        }
    }
}