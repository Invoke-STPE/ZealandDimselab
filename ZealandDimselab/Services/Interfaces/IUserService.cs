using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        ClaimsIdentity CreateClaimIdentity(string email);
        Task DeleteUserAsync(int id);
        Task<bool> EmailInUseAsync(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(User updatedUser);
        Task<bool> ValidateEmail(string email, string password);
    }
}