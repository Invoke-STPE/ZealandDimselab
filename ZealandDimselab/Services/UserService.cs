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
        private readonly Dictionary<int, User> _users;

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
    }
}
