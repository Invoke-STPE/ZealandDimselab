using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.QCRS.Commands.CategoryCommands;
using ZealandDimselab.Domain.QCRS.Queries.CategoryQueries;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/Category
        // Returns all categories.
        [HttpGet]
        public async Task<IEnumerable<CategoryModel>> Get()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }
        // GET: api/Category
        // Returns one category.
        [HttpGet("{id}")]
        public async Task<CategoryModel> Get(int id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(id));
        }
        // POST: api/Category
        // Adds a category.
        [HttpPost]
        public async Task<CategoryModel> Add([FromBody] CategoryModel category)
        {
            return await _mediator.Send(new InsertCategoryCommand(category));
        }
        // DELETE: api/Category
        // Deletes a category.
        [HttpDelete("{id}")]
        public async Task<CategoryModel> Delete(int id)
        {
            return await _mediator.Send(new DeleteCategoryCommand(id));
        }
        // PUT: api/Category
        // Updates a category.
        [HttpPut]
        public async Task<CategoryModel> Update([FromBody] CategoryModel category)
        {
            return await _mediator.Send(new UpdateCategoryCommand(category));
        }
    }
}
