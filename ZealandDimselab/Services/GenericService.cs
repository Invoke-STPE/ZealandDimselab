using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
public class GenericService <T>
{
    private List<T> objectList;
    public IDbService<T> DbService { get; set; }

    public GenericService(IDbService<T> dbService)
    {
        DbService = dbService;
        objectList = dbService.GetObjectsAsync().Result.ToList();
    }

    /// <summary>
    /// Adds an object asynchronously to the Database via DbService
    /// </summary>
    /// <param name="obj">The object to be added to the Database</param>
    /// <returns></returns>
    public async Task AddObjectAsync(T obj)
    {
        objectList.Add(obj);
        await DbService.AddObjectAsync(obj);
    }

    /// <summary>
    /// Returns a single item from the Database with the given id.
    /// </summary>
    /// <param name="id">The id of the item that should be returned</param>
    /// <returns>Returns the item from the Database</returns>
    public async Task<T> GetObjectByIdAsync(int id)
    {
        return await DbService.GetObjectByIdAsync(id);
    }

    public List<T> GetAllObjects()
    {
        return objectList;
    }

    /// <summary>
    /// Receives an item from the Database that is to be deleted. You get the item from the given Id.
    /// If the Id matches and returns an item from the Database, is is then removed from the Database.
    /// </summary>
    /// <param name="id">The Id of the item that is to be deleted</param>
    /// <returns></returns>
    public async Task DeleteObjectAsync(int id)
    {
        T obj = await DbService.GetObjectByIdAsync(id);

        if (obj != null)
        {
            await DbService.DeleteObjectAsync(obj);
            objectList = (await DbService.GetObjectsAsync()).ToList();
        }
    }

    public async Task UpdateObjectAsync(T obj)
    {
        if (obj != null)
        {
            await DbService.UpdateObjectAsync(obj);
            objectList = (await DbService.GetObjectsAsync()).ToList();
        }
    }
    }
}
