using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingService : GenericService<Booking>
    {
        public BookingService(IDbService<Item> dbService) : base(dbService)
        {
            
        }
    }
}
