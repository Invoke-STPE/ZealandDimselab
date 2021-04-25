using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;

namespace ZealandDimselab.MockData
{
    public class MockUsers : IRepository<User>
    {
        public static List<User> users;
        private PasswordHasher<string> passwordHasher;
        public MockUsers()
        {
            passwordHasher = new PasswordHasher<string>();
            users = new List<User>()
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
            throw new NotImplementedException();
        }

        public Task DeleteObjectAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllAsync()
        {
            return users;
        }

        public Task<User> GetObjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(User entity)
        {
            throw new NotImplementedException();
        }

        private string PasswordEncrypt(string password)
        {
            return passwordHasher.HashPassword(null, password);
        }
    }
}
