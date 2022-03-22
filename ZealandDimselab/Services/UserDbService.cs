using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class UserDbService : GenericDbService<User>, IUserDb
    {
        public UserDbService(DimselabDbContext context) : base(context)
        {
        }

        public async Task<bool> DoesEmailExist(string email)
        {
                return _context.Users.Any(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUserByEmail(string email)
        {
                return _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
