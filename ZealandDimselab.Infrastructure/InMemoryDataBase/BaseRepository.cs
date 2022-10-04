using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZealandDimselab.Domain.Interfaces.DataAccess;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public virtual async Task<IEnumerable<T>> GetObjectsAsync()
        {
            return null;
        }

        public virtual async Task<T> GetObjectByKeyAsync(int key)
        {

            return null;

        }

        public virtual async Task<T> InsertAsync(T obj)
        {

            return null;

        }

        public virtual async Task<T> DeleteAsync(int id)
        {

            return null;


        }

        public virtual async Task<T> UpdateAsync(T obj)
        {

            return null;


        }

        protected async Task<T> CommitChangesAsync()
        {
            return null;
        }

    }
}
