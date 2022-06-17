using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.API.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<bool> DoesEmailExist(string email);
    }
}
