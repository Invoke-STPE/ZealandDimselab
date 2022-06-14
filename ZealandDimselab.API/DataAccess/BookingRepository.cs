using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.API.DataAccess.Interfaces;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Lib.JuntionTables;
using ZealandDimselab.API.Context;

namespace ZealandDimselab.API.DataAccess
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(DimselabDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Booking>> GetObjectsAsync()
        {
            List<Booking> bookings;
            
                bookings = await _context.Bookings
                    .Include(u => u.User)
                    .Include(i => i.BookingItems)
                    .ThenInclude(bi => bi.Item).ToListAsync();
            
            return bookings;
        }

        public override async Task<Booking> GetObjectByKeyAsync(int id)
        {
            Booking booking = new Booking();
           
                booking = await _context.Bookings
                    .Include(u => u.User)
                    .Include(i => i.BookingItems)
                    .Where(b => b.Id == id).FirstOrDefaultAsync();

            return booking;
        }
    }
}
