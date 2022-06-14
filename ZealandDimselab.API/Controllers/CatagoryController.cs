using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.API.DataAccess.Interfaces;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repository;

        public CatagoryController(IGenericRepository<Category> genericRepository)
        {
            _repository = genericRepository;
        }
        // GET: api/Category
        // Returns all categories.
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _repository.GetObjectsAsync();
        }
        // GET: api/Category
        // Returns one category.
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await _repository.GetObjectByKeyAsync(id);
        }
        // POST: api/Category
        // Adds a category.
        [HttpPost]
        public async Task Add([FromBody] Category category)
        {
            await _repository.AddObjectAsync(category);
        }
        // DELETE: api/Category
        // Deletes a category.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Category category = await Get(id);
            await _repository.DeleteObjectAsync(category);
        }
        // PUT: api/Category
        // Updates a category.
        [HttpPut]
        public async Task Update([FromBody] Category category)
        {
            await _repository.UpdateObjectAsync(category);
        }
    }
}
