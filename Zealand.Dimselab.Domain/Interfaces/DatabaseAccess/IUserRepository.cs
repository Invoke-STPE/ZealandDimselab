using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.Interfaces.DataAccess
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
        Task<bool> IsEmailAlreadyInUse(string email);
    }
}