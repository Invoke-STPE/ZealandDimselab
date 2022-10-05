using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class ItemRepository : BaseRepository<ItemModel>, IItemRepository
    {
        private List<ItemModel> _items;

        public ItemRepository()
        {
            _items = new List<ItemModel>()
            {
                new ItemModel() { Id = 1,
                    Name = "Test item 1",
                    Description = "This is a test",
                    ImageName = "NoImage.jpg",
                    CategoryId = 1,
                    Category = new CategoryModel {CategoryId = 1, CategoryName = "Test Category 1"},
                    Quantity = 5,
                    Stock = 4
                },
                new ItemModel() { Id = 2,
                    Name = "Test item 2",
                    Description = "This is a test",
                    ImageName = "NoImage.jpg",
                    CategoryId = 2,
                    Category = new CategoryModel {CategoryId = 2, CategoryName = "Test Category 2"},
                    Quantity = 5,
                    Stock = 4
                },
                new ItemModel() { Id = 3,
                    Name = "Test item 3",
                    Description = "This is a test",
                    ImageName = "NoImage.jpg",
                    CategoryId = 3,
                    Category = new CategoryModel {CategoryId = 3, CategoryName = "Test Category 3"},
                    Quantity = 5,
                    Stock = 4
                }
            };
        }
        public async Task<ItemModel> GetItemWithCategoriesAsync(int id)
        {
            return await GetObjectByKeyAsync(id); // Might not be needed 
        }
        public async Task<List<ItemModel>> GetAllItemsWithCategoriesAsync()
        {
            var items = await GetObjectsAsync();
            return items.ToList();
        }
        public async Task<List<ItemModel>> GetItemsWithCategoryId(int id)
        {
            var items = await GetObjectsAsync();
            return items.ToList();
        }

        public async override Task<IEnumerable<ItemModel>> GetObjectsAsync()
        {
            return await Task.FromResult(_items);
        }

        public async override Task<ItemModel> GetObjectByKeyAsync(int id)
        {
            return await Task.FromResult(_items.SingleOrDefault(i => i.Id == id));
        }

        public async override Task<ItemModel> InsertAsync(ItemModel item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            return await Task.FromResult(item);
        }

        public async override Task<ItemModel> DeleteAsync(int id)
        {
            ItemModel item = await GetObjectByKeyAsync(id);
            _items.Remove(item);
            return await Task.FromResult(item);
        }

        public override async Task<ItemModel> UpdateAsync(ItemModel item)
        {
            ItemModel itemToBeUpdated = await GetObjectByKeyAsync(item.Id);
            itemToBeUpdated.Stock = item.Stock;
            itemToBeUpdated.Quantity = item.Quantity;
            itemToBeUpdated.Category = item.Category;
            itemToBeUpdated.CategoryId = item.CategoryId;
            itemToBeUpdated.Description = item.Description;
            itemToBeUpdated.Name = item.Name;
            itemToBeUpdated.ImageName = item.ImageName;

            return itemToBeUpdated;
        }
    }
}
