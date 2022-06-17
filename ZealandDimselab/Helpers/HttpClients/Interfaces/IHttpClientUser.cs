using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Helpers.HttpClients
{
    public interface IHttpClientUser
    {
        Task<UserDto> GetUserByEmailAsync(string email);
        Task AddUserAsync(string paramEmail);
    }
}