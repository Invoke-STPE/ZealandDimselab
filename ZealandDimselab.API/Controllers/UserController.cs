using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.QCRS.Commands.UserCommands;
using ZealandDimselab.Domain.QCRS.Queries.UserQueries;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/User
        // Returns all users.
        [HttpGet]
        public async Task<IEnumerable<UserModel>> Get()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }
        // GET: api/User
        // Returns one user.
        [HttpGet("{id}")]
        public async Task<UserModel> Get(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }
        // GET: api/User/GetUserByEmail
        // Returns one user by email.
        [HttpGet("GetUserByEmail")]
        public async Task<UserModel> GetUserByEmail([FromQuery] string email)
        {
            return await _mediator.Send(new GetUserByEmailQuery(email));
        }
        // GET: api/User/IsEmailInUse
        // Validates if a email already exists in the system.
        [HttpGet("IsEmailInUse")]
        public async Task<bool> IsEmailInUse([FromQuery] string email)
        {
            return await _mediator.Send(new IsEmailInUseQuery(email));
        }
        // POST: api/User
        // Adds a user.
        [HttpPost]
        public async Task<UserModel> Add([FromBody] UserModel user)
        {
            return await _mediator.Send(new InsertUserCommand(user));
        }
        // DELETE: api/User
        // Deletes a user.
        [HttpDelete("{id}")]
        public async Task<UserModel> Delete(int id)
        {
            return await _mediator.Send(new DeleteUserCommand(id));
        }
        // PUT: api/Category
        // Updates a category.
        [HttpPut]
        public async Task<UserModel> Update([FromBody] UserModel user)
        {
            return await _mediator.Send(new UpdateUserCommand(user));
        }

        // 
    }
}
