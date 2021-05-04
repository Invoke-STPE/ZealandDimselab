using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class ItemDbService: GenericDbService<Item>
    {
        public async Task<Item> GetItemWithCategoriesAsync(int id)
        {
            Item item;

            using (var context = new DimselabDbContext())
            {
                item = await context.Items
                    .FirstOrDefaultAsync();
            }
            return item;
        }

        public async Task<List<Item>> GetAllItemsWithCategoriesAsync()
        {
            List<Item> items;

            using (var context = new DimselabDbContext())
            {
                items = await context.Items
                    .ToListAsync();

            }

            return items;
        }
    }
}
