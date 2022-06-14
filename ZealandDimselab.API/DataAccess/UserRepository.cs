using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.API.DataAccess.Interfaces;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.API.Context;
using ZealandDimselab.API.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ZealandDimselab.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DimselabDbContext context) : base(context)
        {
        }

        public async Task<bool> DoesEmailExist(string email)
        {
            bool result = _context.Users.Any(u => u.Email.ToLower() == email.ToLower());
            return await Task.FromResult(result);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
