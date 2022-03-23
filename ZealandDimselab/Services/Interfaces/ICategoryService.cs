using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(int id, Category category);
    }
}