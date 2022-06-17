using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.API.DataAccess.Interfaces;

namespace ZealandDimselab.API.DataAccess.Interfaces
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        public Task<Item> GetItemWithCategoriesAsync(int id);
        public Task<List<Item>> GetAllItemsWithCategoriesAsync();
        public Task<List<Item>> GetItemsWithCategoryId(int id);



    }
}
