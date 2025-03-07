﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.API.DataAccess.Interfaces;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET: api/User
        // Returns all users.
        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await _itemRepository.GetObjectsAsync();
        }
        // GET: api/User
        // Returns one user.
        [HttpGet("{id}")]
        public async Task<Item> Get(int id)
        {
            return await _itemRepository.GetObjectByKeyAsync(id);
        }
        // GET: api/items/GetItemsWithCategories
        // Returns one item with a category.
        [HttpGet("GetItemWithCategories")]
        public async Task<Item> GetItemWithCategories([FromQuery] int id)
        {
            return await _itemRepository.GetItemWithCategoriesAsync(id);
        }
        // GET: api/items/GetAllItemsWithCategories
        // Returns Items with Categories prop filled.
        [HttpGet("GetAllItemsWithCategories")]
        public async Task<IEnumerable<Item>> GetAllItemsWithCategories()
        {
            return await _itemRepository.GetAllItemsWithCategoriesAsync();
        }
        // GET: api/items/GetItemsWithCategoryId
        // Gets all item for a specific category.
        [HttpGet("GetItemsWithCategoryId")]
        public async Task<IEnumerable<Item>> GetItemsWithCategoryId([FromQuery] int id)
        {
            return await _itemRepository.GetItemsWithCategoryId(id);
        }
        // POST: api/items
        // Adds an items.
        [HttpPost]
        public async Task Add([FromBody] Item item)
        {
            await _itemRepository.AddObjectAsync(item);
        }
        // DELETE: api/items
        // Deletes an item.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Item item = await Get(id);
            await _itemRepository.DeleteObjectAsync(item);
        }
        // PUT: api/items
        // Updates an item.
        [HttpPut]
        public async Task Update([FromBody] Item item)
        {
            await _itemRepository.UpdateObjectAsync(item);
        }
    }
}
