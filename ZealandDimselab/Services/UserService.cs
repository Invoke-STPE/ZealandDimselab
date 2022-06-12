﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Services.Interfaces;
using ZealandDimselab.Interfaces;

namespace ZealandDimselab.Services
{
    public class UserService : IUserService
    {
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly IUserRepository dbService;

        public UserService(IUserRepository dbService)
        {
            this.dbService = dbService;
            _passwordHasher = new PasswordHasher<string>();
            // Test user: Admin Admin@Dimselab.dk secret1234 (don't tell anyone)
            // Test user: Oscar Oscar@email.com password
            //User user = new User("Oscar", "Osca2324@edu.easj.dk");
            //User user = new User("Admin@Dimselab.dk", "Admin", "Test1234");
            //AddUser(user);

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

            return await dbService.GetUserByEmail(email); // Checks all users in list "users" if incoming email matches one of them.
        }

        public async Task AddUserAsync(User user)
        {
            //user.Password = _passwordHasher.HashPassword(null, user.Password);
            string[] subs = user.Email.Split("@");

            if (subs[1].ToLower() == "edu.zealand.dk")
            {
                if (!(await EmailInUseAsync(user.Email)))
                {
                    await dbService.AddObjectAsync(user);
                }
            }
            else { await dbService.AddObjectAsync(user); }
        }
        public void AddUser(User user)
        {
            user.Password = _passwordHasher.HashPassword(null, user.Password);
            string[] subs = user.Email.Split("@");


                if (!EmailInUseAsync(user.Email).Result)
                {
                    user.Role = "admin";
                    dbService.AddObjectAsync(user);
                }
            
            else { dbService.AddObjectAsync(user); }
        }

        public async Task DeleteUserAsync(int id)
        {
            await dbService.DeleteObjectAsync(await GetUserByIdAsync(id));
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            await dbService.UpdateObjectAsync(updatedUser);
        }

        public async Task<bool> ValidateEmail(string email, string password)
        {
            if (await EmailInUseAsync(email))
            {
                var user = await GetUserByEmail(email);
                if (user != null)
                {
                    if (PasswordVerification(user.Password, password) == PasswordVerificationResult.Success) // Checks if password matches password.
                    {
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }
        private PasswordVerificationResult PasswordVerification(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }

        public async Task<ClaimsIdentity> CreateClaimIdentity(string email)
        {
            User user = await GetUserByEmail(email);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }

        public async Task<bool> EmailInUseAsync(string email)
        {
            return await dbService.DoesEmailExist(email);
        }

        private string AssignRoleToUser(string[] emailSubs)
        {
            string role = string.Empty;
            switch (emailSubs[0].Length)
            {
                case (8):
                    role = "student";
                    break;
                case (4):
                    role = "teacher";
                    break;
                default:
                    break;
            }
            return role;
        }

    }
}
