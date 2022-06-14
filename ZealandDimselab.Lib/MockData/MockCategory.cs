using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Lib
{
    public class MockCategory
    {
        public Task AddCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllCategoriesAsync()
        {
            return Task.FromResult(new List<Category>()
            {
                new Category {CategoryId = 1, CategoryName = "Droner", ImageName = null},
                new Category {CategoryId = 2, CategoryName = "Kabler", ImageName = null},
                new Category {CategoryId = 3, CategoryName = "Raspberry Pies", ImageName = null},
            });
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
