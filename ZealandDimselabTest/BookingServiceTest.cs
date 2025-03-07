﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;
using ZealandDimselab.Services.Interfaces;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class BookingServiceTest
    {
        private IBookingService bookingService;
        private BookingMockData dbService;
        List<BookingItem> bookingItems;

        [TestInitialize]
        public void InitializeTest()
        {
            dbService = new BookingMockData();
            bookingService = new BookingService(dbService, new MockItemData());


            bookingItems = new List<BookingItem>()
            {
                new BookingItem(new Booking(), new Item("RC car", "fjernstyret bil"))
            };

        }

        [TestMethod]
        public async Task AddBookingAsync_AddValidBooking_IncrementsCount()
        {
            var expectedCount = (await bookingService.GetAllBookingsAsync()).ToList().Count + 1;
            User user = new User("Mike", "Mike@gmail.com", "hejMedDig");
            Booking booking = new Booking(bookingItems, user, "Booking Details", DateTime.Now.Date, DateTime.Now.Date.AddDays(2), false);
            await bookingService.AddBookingAsync(booking);

            // Act
            int actualCount = (await bookingService.GetAllBookingsAsync()).ToList().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public async Task AddBookingAsync_AddBooking_IncrementCount()
        {

            // Arrange
            var expectedCount = 4;
            List<BookingItem> bookingItems = new List<BookingItem>();
            User user = new User("Mikkel", "meilig@.com", "1234");

            Booking booking = new Booking(bookingItems, user, "Details", DateTime.Now, DateTime.Now, false);
            await bookingService.AddBookingAsync(booking);

            // Act
            var actualCount = (await bookingService.GetAllBookingsAsync()).ToList().Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public async Task UpdateBookingAsync_UpdateExsitingBookingUser_ReturnsUpdatedObject()
        {
            // Arrange
            List<BookingItem> bookingItems = new List<BookingItem>();
            Booking booking = (await bookingService.GetBookingByKeyAsync(2));
            User expecteUser = new User("daddycool", "batman@secret.com", "007Jamesbond");


            // Act
            booking.User = expecteUser;
            await bookingService.UpdateBookingAsync(booking);
            Booking actualBooking = await bookingService.GetBookingByKeyAsync(2);

            // Assert
            Assert.AreEqual(expecteUser, actualBooking.User);
        }

        [TestMethod]
        public async Task GetBookingByIdAsync_ValidId_ReturnsBookingObject()
        {
            Booking expectedBooking = new Booking(bookingItems, new User("Simon", "smelly@.com", "1234"), "skal bruges i morgen", DateTime.Now, DateTime.Now, false);
            bookingService.AddBookingAsync(expectedBooking);
            // Act
            Booking actualBooking = await bookingService.GetBookingByKeyAsync(4);

            // Assert
            Assert.AreEqual(expectedBooking, actualBooking);
        }

        //[TestMethod] // Might not be a needed test? 
        //public async Task GetBookingsByEmailAsync_ValidEmail_ReturnsBookingObjects()
        //{
        //    // Arrange
        //    Booking expectedBooking = new Booking(1, bookingItems, new User("Simon", "smelly@.com", "1234"), "skal bruges i morgen", DateTime.Now, DateTime.Now);

        //    // Act
        //    Booking actualBooking = await bookingService.GetBookingByKeyAsync(1); // Mistake here, it looks for id

        //    // Assert
        //    Assert.AreEqual(expectedBooking.Id, actualBooking);
        //}


        public class BookingMockData : IBookingRepository
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

            public async Task<IEnumerable<Booking>> GetObjectsAsync()
            {
                List<Booking> bookings;

                bookings = await dbContext.Bookings
                    .Include(u => u.User)
                    .Include(i => i.BookingItems)
                    .ThenInclude(bi => bi.Item).ToListAsync();
                return bookings;
            }

            public async Task<Booking> GetObjectByKeyAsync(int id)
            {
                return await dbContext.Set<Booking>().FindAsync(id);
            }

            public async Task AddObjectAsync(Booking obj)
            {
                await dbContext.Set<Booking>().AddAsync(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task DeleteObjectAsync(Booking obj)
            {
                dbContext.Set<Booking>().Remove(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task UpdateObjectAsync(Booking obj)
            {
                dbContext.Set<Booking>().Update(obj);
                await dbContext.SaveChangesAsync();
            }


            private async Task LoadDatabase()
            {
                List<Item> item1 = new List<Item>() { new Item("RC car", "fjernstyret bil") };
                List<Item> item2 = new List<Item>() { new Item("vr headset", "glasses") };
                List<Item> item3 = new List<Item>() { new Item("Din Mor", "Er fed") };

                List<BookingItem> bookingItems1 = CreateBookingItems(item1);
                List<BookingItem> bookingItems2 = CreateBookingItems(item2);
                List<BookingItem> bookingItems3 = CreateBookingItems(item3);


                dbContext.Bookings.Add(new Booking(bookingItems1, new User("Simon", "smelly@.com", "1234"), "skal bruges i morgen", DateTime.Now, DateTime.Now, false));
                dbContext.Bookings.Add(new Booking(bookingItems2, new User("Mikkel", "meilig@.com", "1234"), "Details", DateTime.Now, DateTime.Now, false));
                dbContext.Bookings.Add(new Booking(bookingItems3, new User("Oscar", "oscar@.com", "1234"), "Details", DateTime.Now, DateTime.Now, false));

                await dbContext.SaveChangesAsync();
            }

            private List<BookingItem> CreateBookingItems(List<Item> items)
            {
                List<BookingItem> bookingItems = new List<BookingItem>();
                foreach (var item in items)
                {
                    bookingItems.Add(new BookingItem() { Item = item });
                }

                return bookingItems;
            }

            public void TestMethod()
            {
                throw new NotImplementedException();
            }
        }
        internal class MockItemData : IItemRepository
        {
            DimselabDbContext dbContext;

            public MockItemData()
            {
                var options = new DbContextOptionsBuilder<DimselabDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
                dbContext = new DimselabDbContext(options);
                LoadDatabase();
            }

            public async Task<Item> GetItemWithCategoriesAsync(int id)
            {
                Item item;

                using (var context = new DimselabDbContext())
                {
                    item = await context.Items
                        .Include(i => i.Category)
                        .Where(i => i.Id == id)
                        .FirstOrDefaultAsync();
                }

                return item;
            }

            public async Task<List<Item>> GetAllItemsWithCategoriesAsync()
            {
                List<Item> items;

                using (var context = new DimselabDbContext())
                {
                    items = await context.Items
                        .Include(i => i.Category)
                        .ToListAsync();


                }

                return items;
            }

            public async Task<List<Item>> GetItemsWithCategoryId(int id)
            {
                List<Item> items;

                using (var context = new DimselabDbContext())
                {
                    items = await context.Items
                        .Include(i => i.Category)
                        .Where(i => i.CategoryId == id)
                        .ToListAsync();
                }

                return items;
            }

            public async Task<IEnumerable<Item>> GetObjectsAsync()
            {
                return await dbContext.Set<Item>().AsNoTracking().ToListAsync();
            }

            public async Task<Item> GetObjectByKeyAsync(int id)
            {
                return await dbContext.Set<Item>().FindAsync(id);
            }

            public async Task AddObjectAsync(Item obj)
            {
                await dbContext.Set<Item>().AddAsync(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task DeleteObjectAsync(Item obj)
            {
                dbContext.Set<Item>().Remove(obj);
                await dbContext.SaveChangesAsync();
            }

            public async Task UpdateObjectAsync(Item obj)
            {
                dbContext.Set<Item>().Update(obj);
                await dbContext.SaveChangesAsync();
            }

            public void DropDatabase()
            {
                dbContext.Database.EnsureDeleted();
            }

            private void LoadDatabase()
            {
                dbContext.Items.Add(new Item("Test Item 1", "Test Description"));
                dbContext.Items.Add(new Item("Test Item 2", "Test Description"));
                dbContext.Items.Add(new Item("Test Item 3", "Test Description"));
                dbContext.Items.Add(new Item("Test Item 4", "Test Description"));
                dbContext.Items.Add(new Item("Test Item 5", "Test Description"));

                dbContext.SaveChangesAsync();
            }
        }
    }
}
