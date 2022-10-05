using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;
using ZealandDimselab.Domain.Interfaces.DatabaseAccess;

namespace ZealandDimselab.Infrastructure.InMemoryDataBase
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        private List<UserModel> _users;
        public UserRepository()
        {
            _users = new List<UserModel>()
            {
                new UserModel() { Bookings = new List<BookingModel>(), Email = "Steven@email.com", Id = 1, Password = "Test", Role = "admin" },
                new UserModel() { Bookings = new List<BookingModel>(), Email = "Mike@email.com", Id = 2, Password = "Test", Role = "student" },
                new UserModel() { Bookings = new List<BookingModel>(), Email = "Ninette@email.com", Id = 3, Password = "Test", Role = "student" }
            };
        }
        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            return await Task.FromResult(_users.Any(u => u.Email == email));
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Email == email));
        }

        public async override Task<UserModel> GetObjectByKeyAsync(int id)
        {
            return await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }

        public async override Task<IEnumerable<UserModel>> GetObjectsAsync()
        {
            return await Task.FromResult(_users);
        }
        public async override Task<UserModel> InsertAsync(UserModel user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return await Task.FromResult(user);
        }
        public async override Task<UserModel> DeleteAsync(int id)
        {
            UserModel user = _users.FirstOrDefault(u => u.Id == id);
            _users.Remove(user);
            return await Task.FromResult(user);
        }

        public async override Task<UserModel> UpdateAsync(UserModel user)
        {
            UserModel updateUser = await GetObjectByKeyAsync(user.Id);
            updateUser.Email = user.Email;
            updateUser.Role = user.Role;
            updateUser.Bookings = user.Bookings;
            return await Task.FromResult(updateUser);
        }
    }
}
