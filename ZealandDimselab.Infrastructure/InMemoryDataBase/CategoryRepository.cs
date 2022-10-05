using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class CategoryRepository : BaseRepository<CategoryModel>, ICategoryRepository
    {
        private List<CategoryModel> _categories;
        public CategoryRepository()
        {
            _categories = new List<CategoryModel>()
            {
                new CategoryModel {CategoryId = 1, CategoryName = "Test Category 1"},
                new CategoryModel {CategoryId = 2, CategoryName = "Test Category 2"},
                new CategoryModel {CategoryId = 3, CategoryName = "Test Category 3"}
            };
        }
        public override async Task<CategoryModel> InsertAsync(CategoryModel category)
        {
            category.CategoryId = _categories.Count + 1;

            return await Task.FromResult(category);
        }

        public async override Task<IEnumerable<CategoryModel>> GetObjectsAsync()
        {
            return await Task.FromResult(_categories);
        }

        public async override Task<CategoryModel> GetObjectByKeyAsync(int id)
        {
            return await Task.FromResult(_categories.SingleOrDefault(c => c.CategoryId == id));
        }

        public async override Task<CategoryModel> DeleteAsync(int id)
        {
            CategoryModel categoryModel = await GetObjectByKeyAsync(id);
            _categories.Remove(categoryModel);
            return await Task.FromResult(categoryModel);
        }
        public override async Task<CategoryModel> UpdateAsync(CategoryModel category)
        {
            CategoryModel categoryModel = await GetObjectByKeyAsync(category.CategoryId);
            category.CategoryName = categoryModel.CategoryName;
            return await Task.FromResult(categoryModel);
        }
    }
}
