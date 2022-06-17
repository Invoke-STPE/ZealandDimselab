using System.Collections.Generic;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public interface IHttpClientCategory
    {
        Task AddCategoryAsync(CategoryDto category);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(CategoryDto category);
    }
}