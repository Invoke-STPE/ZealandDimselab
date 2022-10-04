using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DataAccess;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            return false;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return null;
        }
    }
}
