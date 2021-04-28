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
        public IDbService<Item> DbService { get; set; }

        public ItemService(IDbService<Item> dbService)
        {
            DbService = dbService;
            _items = dbService.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// Adds an item asynchronously to the Database via DbService
        /// </summary>
        /// <param name="item">The item to be added to the Database</param>
        /// <returns></returns>
        public async Task AddItemAsync(Item item)
        {
            _items.Add(item);
            await DbService.AddObjectAsync(item);
        }

        /// <summary>
        /// Returns a single item from the Database with the given id.
        /// </summary>
        /// <param name="id">The id of the item that should be returned</param>
        /// <returns>Returns the item from the Database</returns>
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await DbService.GetObjectByIdAsync(id);
        }

        public List<Item> GetAllItems()
        {
            return _items;
        }

        /// <summary>
        /// Receives an item from the Database that is to be deleted. You get the item from the given Id.
        /// If the Id matches and returns an item from the Database, is is then removed from the Database.
        /// </summary>
        /// <param name="id">The Id of the item that is to be deleted</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Uses Linq to make a query that should only return the items that matches the query
        /// </summary>
        /// <param name="name">The given name that is filtered by</param>
        /// <returns>Returns the items that match the "name" parameter</returns>
        public IEnumerable<Item> FilterByName(string name)
        {
            return from item in _items
                   where item.Name == name
                   select item;
        }
    }
}