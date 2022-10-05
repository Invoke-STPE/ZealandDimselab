using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.Interfaces.DatabaseAccess
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<bool> IsEmailAlreadyInUse(string email);
    }
}