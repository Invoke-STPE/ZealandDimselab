using System.Collections.Generic;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase
{
    public interface IItemRepository : IBaseRepository<ItemModel>
    {
        Task<List<ItemModel>> GetAllItemsWithCategoriesAsync();
        Task<List<ItemModel>> GetItemsWithCategoryId(int id);
        Task<ItemModel> GetItemWithCategoriesAsync(int id);
    }
}