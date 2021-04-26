using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var expectedCount = 5;

            // Act
            var actualCount = userService.GetUsers().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public async Task AddUserAsync_AddUser_IncrementCount()
        {
            // Arrange
            var expectedCount = 6;
            User user = new User(6, "Mike", "Mike@gmail.com", "Mike1234");
            await userService.AddUserAsync(user);

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
            var expectedCount = 4;
            var id = 1;
            await userService.DeleteUserAsync(id);
            // Act
            var actualCount = userService.GetUsers().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);

        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task DeleteUserAsync_IdDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 70000;
            // Act => Assert
            await userService.DeleteUserAsync(id);

        }

        [TestMethod]
        public async Task UpdateUserAsync_UpdateExsitingUser_ReturnsUpdatedObject()
        {
            // Arrange
            User expectedUser = new User(3, "Hoscar", "Hoscar@gmail.com", "Hoscar1234");
            List<User> users = new List<User>();
            // Act
            await userService.UpdateUserAsync(expectedUser);
            users = userService.GetUsers();
            User actualUser = users.SingleOrDefault(u => u.Id == 3);

            // Assert

            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.Name, actualUser.Name);
            Assert.AreEqual(expectedUser.Password, actualUser.Password);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task UpdateUserAsync_IdDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            User user = new User(700, "Hoscar", "Hoscar@gmail.com", "Hoscar1234");
            // Act => Assert
            await userService.UpdateUserAsync(user);

        }

        [TestMethod]
        public async Task ValidateLogin_ValidLogin_ReturnsTrue()
        {
            // Arrange
            string correctEmail = "Steven@gmail.com";
            string correctPassword = "Steven1234";
            bool expectedLoginResult;
            // Act
            expectedLoginResult = userService.ValidateLogin(correctEmail, correctPassword);

            // Assert

            Assert.IsTrue(expectedLoginResult);

        }

        [TestMethod]
        public async Task ValidateLogin_InvalidPasswordLogin_ReturnsFalse()
        {
            // Arrange
            string correctEmail = "Steven@gmail.com";
            string inCorrectPassword = "StevenIncorrect";
            bool expectedLoginResult;
            // Act
            expectedLoginResult = userService.ValidateLogin(correctEmail, inCorrectPassword);

            // Assert

            Assert.IsFalse(expectedLoginResult);

        }

        [TestMethod]
        public async Task ValidateLogin_InvalidEmailLogin_ReturnsFalse()
        {
            // Arrange
            string inCorrectEmail = "Steven@outlook.com";
            string correctPassword = "Steven1234";
            bool expectedLoginResult;
            // Act
            expectedLoginResult = userService.ValidateLogin(inCorrectEmail, correctPassword);

            // Assert

            Assert.IsFalse(expectedLoginResult);

        }

        [TestMethod]
        public async Task CreateClaim_ValidEmail_ReturnsClaimIdentity()
        {
            // Arrange
            string expectedClaimName = "Steven@outlook.com";
            // Act
            ClaimsIdentity actualClaimIdentity = userService.CreateClaimIdentity(expectedClaimName);

            // Assert

            Assert.AreEqual(expectedClaimName, actualClaimIdentity.Name);

        }

        [TestMethod]
        public async Task CreateClaim_LoginAsAdmin_AddsAdminRoleToClaim()
        {
            // Arrange
            string expectedRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role: admin"; // Why does it append schemas? Is it for intergration with AD?
            string email = "Admin@Dimselab";
            ClaimsIdentity actualClaimIdentity = userService.CreateClaimIdentity(email);
            // Act
            string actualRole = actualClaimIdentity.Claims.FirstOrDefault(role => role.Value== "admin").ToString();

            // Assert
            Assert.AreEqual(expectedRole, actualRole);
        }

        [TestMethod]
        public async Task CreateClaim_LoginAsUser_DoesNotAddAdminRoleToClaim()
        {
            // Arrange
            string email = "Steven@gmail.com";
            ClaimsIdentity actualClaimIdentity = userService.CreateClaimIdentity(email);
            // Act
            Claim claimRole = actualClaimIdentity.Claims.FirstOrDefault(role => role.Value == "admin");
            // Assert
            Assert.IsNull(claimRole);
        }

        internal class UserMockData : IRepository<User>
        {
            private static List<User> _users;
            private readonly PasswordHasher<string> passwordHasher;

            public UserMockData()
            {
                passwordHasher = new PasswordHasher<string>();
                _users = new List<User>()
            {
                new User(1, "Steven", "Steven@gmail.com", PasswordEncrypt("Steven1234")),
                new User(2, "Mikkel", "Mikkel@gmail.com", PasswordEncrypt("Mikkel1234")),
                new User(3, "Oscar", "Oscar@gmail.com", PasswordEncrypt("Oscar1234")),
                new User(4, "Christopher", "Christopher@gmail.com", PasswordEncrypt("Christopher1234")),
                new User(5, "Admin", "Admin@Dimselab", PasswordEncrypt("Admin1234")),
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
                await Task.Run(() => user.Email = entity.Email);
                await Task.Run(() => user.Name = entity.Name);
                await Task.Run(() => user.Password = entity.Password);
             
            }

            private string PasswordEncrypt(string password)
            {
                return passwordHasher.HashPassword(null, password);
            }
        }
    }
}
