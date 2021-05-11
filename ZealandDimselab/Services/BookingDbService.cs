using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingDbService : GenericDbService<Booking>, IDbService<Booking>
    {

        public override async Task<IEnumerable<Booking>> GetObjectsAsync()
        {
            List<Booking> bookings;
            using (var context = new DimselabDbContext())
            {
                bookings = context.Bookings
                    .Include(u => u.User)
                    .Include(i => i.BookingItems)
                    .ThenInclude(bi => bi.Item).ToList();
            }
            return bookings;
        }
    }
}
