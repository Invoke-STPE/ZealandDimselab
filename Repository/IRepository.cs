using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZealandDimselab.Repository
{
    public interface IRepository<T> where T :class
    {
        public List<T> GetAllAsync();
        public Task UpdateObjectAsync(T entity);
        public Task DeleteObjectAsync(T entity);
        public Task AddObjectAsync(T entity);
    }
}