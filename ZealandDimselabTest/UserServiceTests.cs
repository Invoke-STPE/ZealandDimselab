using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class UserServiceTests
    {
        private IRepository<User> repository;
        private UserService userService;

        [TestInitialize]
        public void InitializeTest()
        {
            repository = new UserMockData();
            userService = new UserService(repository);
        }
    }

    internal class UserMockData : IRepository<User>
    {
        public Task AddObjectAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteObjectAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllAsync()
        {
            return new List<User>()
            {
                new User(1, "Steven", "Steven@gmail.com", "Steven1234"),
                new User(2, "Mikkel", "Mikkel@gmail.com", "Mikkel1234"),
                new User(3, "Oscar", "Oscar@gmail.com", "Oscar1234"),
                new User(1, "Christopher", "Christopher@gmail.com", "Christopher1234"),
            };
        }

        public Task<User> GetObjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
