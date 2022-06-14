using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.API.DataAccess.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
    }
}
