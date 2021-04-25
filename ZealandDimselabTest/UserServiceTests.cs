using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void GetUsers_Default_ReturnsAllUsers()
        {
            // Arrange
            var expectedCount = 4;

            // Act
            var actualCount = userService.GetUsers().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void AddUserAsync_AddUser_IncrementCount()
        {
            // Arrange
            var expectedCount = 5;
            User user = new User(5, "Mike", "Mike@gmail.com", "Mike1234");
            userService.AddUserAsync(user);

            // Act
            var actualCount = userService.GetUsers().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AddUserAsync_IdAlreadyExists_ThrowsArgumentException()
        {
            // Arrange
            User user = new User(2, "Mike", "Mike@gmail.com", "Mike1234");
            // Act => Assert
            await userService.AddUserAsync(user);

        }

        [TestMethod]
        public async Task DeleteUserAsync_RemovesUser_DecreasesCount()
        {
            // Arrange
            var expectedCount = 3;
            var id = 1;
            await userService.DeleteUserAsync(id);
            // Act
            var actualCount = userService.GetUsers().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);

        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task DeleteUserAsyn_IdDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 70000;
            // Act => Assert
            await userService.DeleteUserAsync(id);

        }





        internal class UserMockData : IRepository<User>
        {
            private static List<User> _users;

            public UserMockData()
            {
                _users = new List<User>()
            {
                new User(1, "Steven", "Steven@gmail.com", "Steven1234"),
                new User(2, "Mikkel", "Mikkel@gmail.com", "Mikkel1234"),
                new User(3, "Oscar", "Oscar@gmail.com", "Oscar1234"),
                new User(4, "Christopher", "Christopher@gmail.com", "Christopher1234"),
            };
            }
            public Task AddObjectAsync(User entity)
            {
                return Task.CompletedTask;
            }

            public Task DeleteObjectAsync(User entity)
            {
                return Task.CompletedTask;
            }

            public List<User> GetAllAsync()
            {
                return _users;
            }

            public Task<User> GetObjectByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public async Task UpdateObjectAsync(User entity)
            {
                User user = _users.SingleOrDefault(u => u.Id == entity.Id);
                await Task.Run(() => user = entity);
            }
        }
    }
}
