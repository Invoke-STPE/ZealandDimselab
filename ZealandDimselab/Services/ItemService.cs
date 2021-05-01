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
        public ItemService(IDbService<Item> dbService) : base(dbService)
        {

        }

        public async Task AddItemAsync(Item item)
        {
            await AddObjectAsync(item);
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
        }

        public List<Item> GetAllItems()
        {
        }

        public async Task DeleteItemAsync(int id)
        {
        }

        public async Task UpdateItemAsync(int id, Item item)
        {
        }
        
        public IEnumerable<Item> FilterByName(string name)
        {
            return from item in _items
                   where item.Name == name
                   select item;
        }
    }
}