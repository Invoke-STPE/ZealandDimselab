using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class GenericService <T>
    {
        private List<T> _objectList;
        private IDbService<Item> dbService;

        public IDbService<T> DbService { get; set; }

        public GenericService(IDbService<T> dbService)
        {
            DbService = dbService;
            _objectList = dbService.GetObjectsAsync().Result.ToList();
        }

        //public List<T> GetAllBookingsTest()
        //{
        //    DbService
        //}

        public GenericService(IDbService<Item> dbService)
        {
            this.dbService = dbService;
        }

        public List<T> GetAllObjects()
        {
            return _objectList;
        }

        /// <summary>
        /// Returns the object with the matching key from the database
        /// </summary>
        /// <param name="key">The key of the object that should be returned</param>
        /// <returns>Returns the object from the Database</returns>
        public async Task<T> GetObjectByKeyAsync(int key)
        {
            return await DbService.GetObjectByKeyAsync(key);
        }


        /// <summary>
        /// Adds an object asynchronously to the Database via DbService
        /// </summary>
        /// <param name="obj">The object to be added to the Database</param>
        /// <returns></returns>
        public async Task AddObjectAsync(T obj)
        {
            _objectList.Add(obj);
            await DbService.AddObjectAsync(obj);
        }



        /// <summary>
        /// Deletes the received object from the database
        /// </summary>
        /// <param name="obj">The object to remove from the database</param>
        /// <returns></returns>
        public async Task DeleteObjectAsync(T obj)
        {
            if (obj != null)
            {
                await DbService.DeleteObjectAsync(obj);
                _objectList = (await DbService.GetObjectsAsync()).ToList();
            }
        }


        /// <summary>
        /// Updates an object via the database
        /// </summary>
        /// <param name="obj">The updated object</param>
        /// <returns></returns>
        public async Task UpdateObjectAsync(T obj)
        {
            if (obj != null)
            {
                await DbService.UpdateObjectAsync(obj);
                _objectList = (await DbService.GetObjectsAsync()).ToList();
            }
        }
    }
}
