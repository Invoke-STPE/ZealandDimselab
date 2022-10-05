using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.QCRS.Commands.ItemCommands;
using ZealandDimselab.Domain.QCRS.Queries.ItemQueries;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/User
        // Returns all users.
        [HttpGet]
        public async Task<IEnumerable<ItemModel>> Get()
        {
            return await _mediator.Send(new GetAllItemsQuery());
        }
        // GET: api/User
        // Returns one user.
        [HttpGet("{id}")]
        public async Task<ItemModel> Get(int id)
        {
            return await _mediator.Send(new GetItemByIdQuery(id));
        }
        // GET: api/items/GetItemsWithCategories
        // Returns one item with a category.
        [HttpGet("GetItemWithCategories")]
        public async Task<ItemModel> GetItemWithCategories([FromQuery] int id)
        {
            return await _mediator.Send(new GetItemByIdQuery(id));
        }
        // GET: api/items/GetAllItemsWithCategories
        // Returns Items with Categories prop filled.
        [HttpGet("GetAllItemsWithCategories")]
        public async Task<IEnumerable<ItemModel>> GetAllItemsWithCategories()
        {
            return await _mediator.Send(new GetAllItemsQuery());
        }
        // GET: api/items/GetItemsWithCategoryId
        // Gets all item for a specific category.
        [HttpGet("GetItemsWithCategoryId")]
        public async Task<IEnumerable<ItemModel>> GetItemsWithCategoryId([FromQuery] int id)
        {
            return await _mediator.Send(new GetAllItemsQuery());
        }
        // POST: api/items
        // Adds an items.
        [HttpPost]
        public async Task<ItemModel> Add([FromBody] ItemModel item)
        {
            return await _mediator.Send(new InsertItemCommand(item));
        }
        // DELETE: api/items
        // Deletes an item.
        [HttpDelete("{id}")]
        public async Task<ItemModel> Delete(int id)
        {
            return await _mediator.Send(new DeleteItemCommand(id));
        }
        // PUT: api/items
        // Updates an item.
        [HttpPut]
        public async Task<ItemModel> Update([FromBody] ItemModel item)
        {
            return await _mediator.Send(new UpdateItemCommand(item));
        }
    }
}
