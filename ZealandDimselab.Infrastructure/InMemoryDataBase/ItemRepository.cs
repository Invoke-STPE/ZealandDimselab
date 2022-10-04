using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class ItemRepository : BaseRepository<ItemModel>, IItemRepository
    {
        public Task<ItemModel> GetItemWithCategoriesAsync(int id)
        {
            return null;
        }
        public Task<List<ItemModel>> GetAllItemsWithCategoriesAsync()
        {
            return null;
        }
        public Task<List<ItemModel>> GetItemsWithCategoryId(int id)
        {
            return null;
        }
    }
}
