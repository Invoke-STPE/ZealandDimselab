using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class CategoryService: GenericService<Category>
    {
        public CategoryService(IDbService<Category> dbService) : base(dbService)
        {
        }

        public List<Category> GetAllCategories()
        {
            return GetAllObjects();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await GetObjectByKeyAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await AddObjectAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await DeleteObjectAsync(await GetCategoryByIdAsync(id));
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            category.CategoryId = id;
            await UpdateObjectAsync(category);
        }
    }
}
