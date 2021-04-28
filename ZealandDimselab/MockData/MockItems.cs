using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;

namespace ZealandDimselab.MockData
{
    public class MockItem : IRepository<Item>
    {
        public static List<Item> items;
        public MockItem()
        {
            items = new List<Item>()
            {
                new Item( "Super cool wire", "Green wire, but it's cool"),
                new Item( "Lame wire", "Red wire"),
                new Item( "Big TV", "120 inch tv"),
            };
        }
        public async Task AddObjectAsync(Item entity)
        {
            items.Add(entity);
        }

        public async Task DeleteObjectAsync(Item entity)
        {
            throw new NotImplementedException();
        }

        public List<Item> GetAllAsync()
        {
            return null;
            throw new NotImplementedException();
            
        }

        public async Task<Item> GetObjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateObjectAsync(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}