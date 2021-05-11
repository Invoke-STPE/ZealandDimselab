using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class UserService
    {
        private PasswordHasher<string> _passwordHasher;
        private readonly IDbService<User> dbService;

        public UserService(IBookingDb<User> dbService)
        {
            this.dbService = dbService;
            _passwordHasher = new PasswordHasher<string>();
            // Test user: Admin Admin@Dimselab.dk secret1234 (don't tell anyone)
            // Test user: Oscar Oscar@email.com password
            //User user = new User("Oscar", "Oscar@email.com", "password");
            //AddUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await dbService.GetObjectsAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await dbService.GetObjectByKeyAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var users = await GetUsersAsync();
            return users.SingleOrDefault(u => u.Email == email); // Checks all users in list "users" if incoming email matches one of them.
        }

        public async Task AddUserAsync(User user)
        {
            user.Password = _passwordHasher.HashPassword(null, user.Password);
            await dbService.AddObjectAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            User user = await GetUserByIdAsync(id);
            await dbService.DeleteObjectAsync(await GetUserByIdAsync(id));
        }
        
        public async Task UpdateUserAsync(User updatedUser)
        {
            await dbService.UpdateObjectAsync(updatedUser);
        }

        public async Task<bool> ValidateLogin(string email, string password)
        {
            var user = await GetUserByEmail(email);
            if (user != null)
            {
                if (PasswordVerification(user.Password, password) == PasswordVerificationResult.Success) // Checks if password matches password.
                {
                    return true;
                }
            }
            return false;
        }
        private PasswordVerificationResult PasswordVerification(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }

        public ClaimsIdentity CreateClaimIdentity(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };

            if (email.ToLower() == "Admin@Dimselab.dk".ToLower()) // This checks if the user attempts to login as an administrator account.
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }
        


    }
}
