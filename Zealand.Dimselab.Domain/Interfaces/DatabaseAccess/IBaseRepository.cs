using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZealandDimselab.Domain.Interfaces.DatabaseAccess
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> DeleteAsync(int id);
        Task<T> GetObjectByKeyAsync(int key);
        Task<IEnumerable<T>> GetObjectsAsync();
        Task<T> InsertAsync(T obj);
        Task<T> UpdateAsync(T obj);
    }
}