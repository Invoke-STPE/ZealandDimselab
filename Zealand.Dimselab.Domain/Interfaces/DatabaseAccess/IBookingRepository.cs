using System.Collections.Generic;
using System.Threading.Tasks;
using Zealand.Dimselab.Domain.Models;

namespace ZealandDimselab.Domain.Interfaces.DataAccess.InMemoryDataBase
{
    public interface IBookingRepository : IBaseRepository<BookingModel>
    {
        Task<List<BookingModel>> GetBookingsByEmailAsync(string email);
        Task<BookingModel> GetObjectByKeyAsync(int id);
        Task<IEnumerable<BookingModel>> GetObjectsAsync();
    }
}