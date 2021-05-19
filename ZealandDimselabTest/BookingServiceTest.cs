//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ZealandDimselab.Interfaces;
//using ZealandDimselab.Models;
//using ZealandDimselab.Services;

//namespace ZealandDimselabTest
//{
//    [TestClass]
//    public class BookingServiceTest
//    {
//        private BookingService bookingService;
//        private BookingMockData dbService;
//        List<BookingItem> bookingItems;

//        [TestInitialize]
//        public void InitializeTest()
//        {
//            dbService = new BookingMockData();
//            bookingService = new BookingService(dbService);
//        }

//        //AddBookingTestCases
//        //[TestMethod]
//        //public async Task AddBookingAsync_CountAsync()
//        //{
//        //    var expectedCount = bookingService.GetAllBookings().Count + 1;

//        //    await bookingService.AddBookingAsync(booking);

//        //    var actualValueDb = (await dbService.GetObjectsAsync()).Count();
//        //    var actualValueList = bookingService.GetAllBookings().Count;

//        //    Assert.AreEqual(expectedCount, actualValueDb);
//        //    Assert.AreEqual(expectedCount, actualValueList);
//        //}

//        [TestMethod]
//        public async Task AddBookingAsync_AddBooking_IncrementCount()
//        {

//            // Arrange
//            var expectedCount = 4;
//            List<BookingItem> bookingItems = new List<BookingItem>();
//            User user = new User("Mikkel", "meilig@.com", "1234");

//            Booking booking = new Booking(bookingItems, user, "Details", DateTime.Now, DateTime.Now);
//            await bookingService.AddBookingAsync(booking);

//            // Act
//            var actualCount = bookingService.GetAllBookings().Result.ToList().Count;

//            // Assert
//            Assert.AreEqual(expectedCount, actualCount);
//        }

//        [TestMethod]
//        public async Task UpdateBookingAsync_UpdateExsitingBooking_ReturnsUpdatedObject()
//        {
//            // Arrange
//            List<BookingItem> bookingItems = new List<BookingItem>();
//            Booking booking = await bookingService.GetBookingByKeyAsync(2);
//            Booking booking = new Booking(bookingItems, user, "Details", DateTime.Now, DateTime.Now);
//            User expecteUser = new User("daddycool", "batman@secret.com", "007Jamesbond");


//            // Act
//            booking.Items = expecteItems;
//            booking.User = expecteUser;
//            await bookingService.UpdateBookingAsync(booking);
//            Booking actualBooking = await bookingService.GetBookingByKeyAsync(2);

//            // Assert
//            Assert.AreEqual(expecteItems, actualBooking.Items);
//            Assert.AreEqual(expecteUser, actualBooking.User);
//        }

//        //[TestMethod]
//        //public async Task GetBookingByIdAsync_ValidId_ReturnsBookingObject()
//        //{
//        //    string expectedDetials = "Details";

//        //    // Act
//        //    Booking actualBooking = await bookingService.GetBookingByKeyAsync(3);

//        //    // Assert
//        //    Assert.AreEqual(expectedDetials, actualBooking.Details);

//        //}

//        //[TestMethod]
//        //public async Task GetBookingByEmailAsync_ValidEmail_ReturnsBookingObject()
//        //{
//        //    //Arrange
//        //    string expectedEmail = "smelly@.com";
//        //    string expectedBookingDetails = "skal bruges i morgen";
//        //    Booking expecBooking = new Booking(new List<Item>() { new Item("RC car", "fjernstyret bil") }, new User("Simon", "smelly@.com", "1234"), "skal bruges i morgen", DateTime.Now, DateTime.Now);
//        //    List<Booking> expectedBookings = new List<Booking>() { expecBooking };

//        //    // Act
//        //    List<Booking> actualBookings = bookingService.GetBookingsByEmail(expectedEmail);

//        //    // Assert
//        //    Assert.AreEqual(expectedBookings, expectedBookings);
//        //}


//        public class BookingMockData : IBookingDb
//        {
//            private DimselabDbContext dbContext;

//            public BookingMockData()
//            {
//                var options = new DbContextOptionsBuilder<DimselabDbContext>()
//                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                    .Options;
//                dbContext = new DimselabDbContext(options);
//                LoadDatabase();
//            }

//            public async Task<IEnumerable<Booking>> GetObjectsAsync()
//            {
//                List<Booking> bookings;

//                bookings = await dbContext.Bookings
//                    .Include(u => u.User)
//                    .Include(i => i.BookingItems)
//                    .ThenInclude(bi => bi.Item).ToListAsync();
//                return bookings;
//            }

//            public async Task<Booking> GetObjectByKeyAsync(int id)
//            {
//                return await dbContext.Set<Booking>().FindAsync(id);
//            }

//            public async Task AddObjectAsync(Booking obj)
//            {
//                await dbContext.Set<Booking>().AddAsync(obj);
//                await dbContext.SaveChangesAsync();
//            }

//            public async Task DeleteObjectAsync(Booking obj)
//            {
//                dbContext.Set<Booking>().Remove(obj);
//                await dbContext.SaveChangesAsync();
//            }

//            public async Task UpdateObjectAsync(Booking obj)
//            {
//                dbContext.Set<Booking>().Update(obj);
//                await dbContext.SaveChangesAsync();
//            }


//            private async Task LoadDatabase()
//            {
//                List<Item> item1 = new List<Item>() { new Item("RC car", "fjernstyret bil") };
//                List<Item> item2 = new List<Item>() { new Item("vr headset", "glasses") };
//                List<Item> item3 = new List<Item>() { new Item("Din Mor", "Er fed") };

//                List<BookingItem> bookingItems1 = CreateBookingItems(item1);
//                List<BookingItem> bookingItems2 = CreateBookingItems(item2);
//                List<BookingItem> bookingItems3 = CreateBookingItems(item3);


//                dbContext.Bookings.Add(new Booking(bookingItems1, new User("Simon", "smelly@.com", "1234"), "skal bruges i morgen", DateTime.Now, DateTime.Now));
//                dbContext.Bookings.Add(new Booking(bookingItems2, new User("Mikkel", "meilig@.com", "1234"), "Details", DateTime.Now, DateTime.Now));
//                dbContext.Bookings.Add(new Booking(bookingItems3, new User("Oscar", "oscar@.com", "1234"), "Details", DateTime.Now, DateTime.Now));

//                await dbContext.SaveChangesAsync();
//            }

//            private List<BookingItem> CreateBookingItems(List<Item> items)
//            {
//                List<BookingItem> bookingItems = new List<BookingItem>();
//                foreach (var item in items)
//                {
//                    bookingItems.Add(new BookingItem() { Item = item });
//                }

//                return bookingItems;
//            }

//            public void TestMethod()
//            {
//                throw new NotImplementedException();
//            }
//        }
//    }
//}
