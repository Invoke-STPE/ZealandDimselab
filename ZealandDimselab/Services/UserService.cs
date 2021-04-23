using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;

namespace ZealandDimselab.Services
{
    public class UserService
    {
        private readonly IRepository<User> repository;
        private Dictionary<int, User> _users;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
            _users = repository.GetAllAsync().ToDictionary(u => u.Id);
        }

        public List<User> GetUsers()
        {
            return _users.Values.ToList();
        }
        public async Task AddUserAsync(User user)
        {
            _users.Add(user.Id, user);
            await repository.AddObjectAsync(user);
    
        }

        public async Task DeleteUserAsync(int id)
        {
            await repository.DeleteObjectAsync(_users[id]);
            _users.Remove(id);
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            if (user != null)
            {
                await repository.UpdateObjectAsync(user);
                _users = repository.GetAllAsync().ToDictionary(i => i.Id);
            }
        }


    }
}
