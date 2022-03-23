using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Models;
using ZealandDimselab.Interfaces;

namespace ZealandDimselab.Services
{
    public class ItemRepository: GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(DimselabDbContext context) : base(context)
        {
        }

        public async Task<Item> GetItemWithCategoriesAsync(int id)
        {
            Item item;
                item = await _context.Items
                    .Include(i => i.Category)
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();

            return item;
        }

        public async Task<List<Item>> GetAllItemsWithCategoriesAsync()
        {
            List<Item> items;

                items = await _context.Items
                    .Include(i => i.Category)
                    .ToListAsync();
            return items;
        }

        public async Task<List<Item>> GetItemsWithCategoryId(int id)
        {
            List<Item> items;

          
                items = await _context.Items
                    .Include(i => i.Category)
                    .Where(i => i.CategoryId == id)
                    .ToListAsync();
            return items;
        }

    }
}
