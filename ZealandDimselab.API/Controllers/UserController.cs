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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        // Returns all users.
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userRepository.GetObjectsAsync();
        }
        // GET: api/User
        // Returns one user.
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _userRepository.GetObjectByKeyAsync(id);
        }
        // GET: api/User/GetUserByEmail
        // Returns one user by email.
        [HttpGet("GetUserByEmail")]
        public async Task<User> GetUserByEmail([FromQuery] string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        // GET: api/User/IsEmailInUse
        // Validates if a email already exists in the system.
        [HttpGet("IsEmailInUse")]
        public async Task<bool> IsEmailInUse([FromQuery] string email)
        {
            return await _userRepository.DoesEmailExist(email);
        }
        // POST: api/User
        // Adds a user.
        [HttpPost]
        public async Task Add([FromBody] User user)
        {
            await _userRepository.AddObjectAsync(user);
        }
        // DELETE: api/User
        // Deletes a user.
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            User user = await Get(id);
            await _userRepository.DeleteObjectAsync(user);
        }
        // PUT: api/Category
        // Updates a category.
        [HttpPut]
        public async Task Update([FromBody] User user)
        {
            await _userRepository.UpdateObjectAsync(user);
        }

        // 
    }
}
