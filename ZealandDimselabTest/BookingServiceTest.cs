using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class BookingServiceTest
    {
        private BookingService bookingService;
        private IDbService<Booking> dbService;
        private Booking booking;

        [TestInitialize]
        public void InitializeTest()
        {
            dbService = new BookingMockData<Booking>();
            bookingService = new BookingService(dbService);

            List<Item> Items = new List<Item>() {new Item("Raspery Pi", "minicomputer")};
            User user = new User(1, "Mikkel", "meilig@.com", "1234");
            booking = new Booking(1, Items, user, "Details", DateTime.Now, DateTime.Now);
        }

        //AddBookingTestCases
        //[TestMethod]
        //public async Task AddBookingAsync_CountAsync()
        //{
        //    var expectedCount = bookingService.GetAllBookings().Count + 1;

        //    await bookingService.AddBookingAsync(booking);

        //    var actualValueDb = (await dbService.GetObjectsAsync()).Count();
        //    var actualValueList = bookingService.GetAllBookings().Count;

        //    Assert.AreEqual(expectedCount, actualValueDb);
        //    Assert.AreEqual(expectedCount, actualValueList);
        //}

        [TestMethod]
        public async Task AddBookingAsync_AddBooking_IncrementCount()
        {
            // Arrange
            var expectedCount = 4;

            List<Item> Items = new List<Item>() { new Item("Raspery Pi", "minicomputer") };
            User user = new User(1, "Mikkel", "meilig@.com", "1234");
            
            Booking booking = new Booking(1, Items, user, "Details", DateTime.Now, DateTime.Now);
            await bookingService.AddBookingAsync(booking);

            // Act
            var actualCount = bookingService.GetAllBookings().ToList().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        public class BookingMockData<T> : IDbService<T> where T : class
        {
            private DimselabDbContext dbContext;

            public BookingMockData()
            {
                var options = new DbContextOptionsBuilder<DimselabDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                dbContext = new DimselabDbContext(options);
                LoadDatabase();
            }

            public async Task<IEnumerable<T>> GetObjectsAsync()
            {
                return await dbContext.Set<T>().AsNoTracking().ToListAsync();
            }

            public async Task AddObjectAsync(T obj)
            {
                await dbContext.Set<T>().AddAsync(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task DeleteObjectAsync(T obj)
            {
                dbContext.Set<T>().Remove(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task UpdateObjectAsync(T obj)
            {
                dbContext.Set<T>().Update(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task<T> GetObjectByKeyAsync(int id)
            {
                return await dbContext.Set<T>().FindAsync(id);
            }

            private void LoadDatabase()
            {

                dbContext.Bookings.Add(new Booking(2, new List<Item>(){new Item("RC car", "fjernstyret bil")},new User("Simon", "smelly@.com", "1234"),"skal bruges i morgen", DateTime.Now, DateTime.Now));
                dbContext.Bookings.Add(new Booking(3, new List<Item>() { new Item("vr headset", "glasses") }, new User(1, "Mikkel", "meilig@.com", "1234"), "Details", DateTime.Now, DateTime.Now));
                dbContext.Bookings.Add(new Booking(4, new List<Item>() { new Item("Din Mor", "Er fed") }, new User(1, "Oscar", "oscar@.com", "1234"), "Details", DateTime.Now, DateTime.Now));

                dbContext.SaveChangesAsync();
            }

        }
    }
}
