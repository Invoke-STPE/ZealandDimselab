using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class GenericServiceTest
    {
        private IBookingDb<Item> _repositoryItem;
        private GenericService<Item> _genericService;
        private List<Item> _objectList;

        [TestInitialize]
        public void InitializeTest()
        {
            _repositoryItem = new MockData<Item>();
            _genericService = new GenericService<Item>(_repositoryItem);
            _objectList = _genericService.GetAllObjects();
        }

        [TestMethod]
        public void GetAllObjects_Count() //GetAllObjects is in InitializeTest
        {
            Assert.AreEqual(_objectList.Count, 5);
        }

        [TestMethod]
        public void GetAllObjects_CorrectObjects() //GetAllObjects is in InitializeTest
        {
            Item firstItem = new Item("Test Item 1", "Test Description") { Id = 1 };
            Assert.IsTrue(firstItem.Equals(_objectList[0]));
            Item secondItem = new Item("Test Item 2", "Test Description") { Id = 2 };
            Assert.IsTrue(secondItem.Equals(_objectList[1]));
            Item thirdItem = new Item("Test Item 3", "Test Description") { Id = 3 };
            Assert.IsTrue(thirdItem.Equals(_objectList[2]));
            Item fourthItem = new Item("Test Item 4", "Test Description") { Id = 4 };
            Assert.IsTrue(fourthItem.Equals(_objectList[3]));
            Item fifthItem = new Item("Test Item 5", "Test Description") { Id = 5 };
            Assert.IsTrue(fifthItem.Equals(_objectList[4]));
        }

        [TestMethod]
        public async Task GetObjectByKeyAsync_ValidKey()
        {
            Item item = await _genericService.GetObjectByKeyAsync(1);
            Item firstItem = new Item("Test Item 1", "Test Description") { Id = 1 };

            Assert.IsTrue(firstItem.Equals(item));
        }

        [TestMethod]
        public async Task GetObjectByKeyAsync_InvalidKey()
        {
            Item item = await _genericService.GetObjectByKeyAsync(6);
            // item does not exist, so should have returned null
            Assert.IsNull(item);
        }

        [TestMethod]
        public async Task AddObjectAsync_ValidObject()
        {
            Item newItem = new Item("Test Item 6", "Test Description");
            await _genericService.AddObjectAsync(newItem);
            Item addedItem = _genericService.GetAllObjects().Last();

            Assert.AreEqual(newItem.Name, addedItem.Name);
            Assert.AreEqual(newItem.Description, addedItem.Description);
        }

        //// Does not throw errors when used on In-Memory Database
        //[TestMethod]
        //public async Task AddObjectAsync_InvalidObject()
        //{
        //    Item newItem = new Item(null, null);

        //    await _genericService.AddObjectAsync(newItem);
        //}

        [TestMethod]
        public async Task DeleteObjectAsync_ValidObject()
        {
            int expectedCount = _genericService.GetAllObjects().Count - 1;
            await _genericService.DeleteObjectAsync(await _genericService.GetObjectByKeyAsync(5));
            int newCount = _genericService.GetAllObjects().Count;

            Assert.AreEqual(newCount, expectedCount);

            // Check that it has deleted the correct object
            Item firstItem = new Item("Test Item 1", "Test Description") { Id = 1 };
            Assert.IsTrue(firstItem.Equals(_objectList[0]));
            Item secondItem = new Item("Test Item 2", "Test Description") { Id = 2 };
            Assert.IsTrue(secondItem.Equals(_objectList[1]));
            Item thirdItem = new Item("Test Item 3", "Test Description") { Id = 3 };
            Assert.IsTrue(thirdItem.Equals(_objectList[2]));
            Item fourthItem = new Item("Test Item 4", "Test Description") { Id = 4 };
            Assert.IsTrue(fourthItem.Equals(_objectList[3]));
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public async Task DeleteObjectAsync_InvalidObject()
        {
            // Item is not in the database, so should throw an exception
            await _genericService.DeleteObjectAsync(new Item("New Item", "Test Description"));
        }

        [TestMethod]
        public async Task DeleteObjectAsync_NullObject()
        {
            int expectedCount = _genericService.GetAllObjects().Count; // Count should not change
            await _genericService.DeleteObjectAsync(await _genericService.GetObjectByKeyAsync(6));
            int newCount = _genericService.GetAllObjects().Count;

            Assert.AreEqual(newCount, expectedCount);
        }

        [TestMethod]
        public async Task UpdateObjectAsync_ValidObject()
        {
            Item item = await _genericService.GetObjectByKeyAsync(5);
            item.Name = "New Name";

            await _genericService.UpdateObjectAsync(item);

            Assert.AreEqual(await _genericService.GetObjectByKeyAsync(5), item);
        }

        [TestMethod]
        public async Task UpdateObjectAsync_NewObject_NoKey()
        {
            Item item = new Item("New Item", "Test Description");

            await _genericService.UpdateObjectAsync(item);

            // Items with an empty key get added to the database
            Assert.AreEqual(6, _genericService.GetAllObjects().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public async Task UpdateObjectAsync_NewObject_WithKey()
        {
            Item item = new Item("New Item", "Test Description") {Id = -1};

            await _genericService.UpdateObjectAsync(item);

            // Items with a key that does not exist in the database should throw an exception
            Assert.AreEqual(6, _genericService.GetAllObjects().Count);
        }


        internal class MockData<T> : IBookingDb<T> where T : class
        {
            DimselabDbContext dbContext;

            public MockData()
            {
                var options = new DbContextOptionsBuilder<DimselabDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
                dbContext = new DimselabDbContext(options);
                LoadDatabase();
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

            public async Task<T> GetObjectByKeyAsync(int id)
            {
                return await dbContext.Set<T>().FindAsync(id);
            }

            public async Task<IEnumerable<T>> GetObjectsAsync()
            {
                return await dbContext.Set<T>().AsNoTracking().ToListAsync();
            }

            public async Task UpdateObjectAsync(T obj)
            {
                dbContext.Set<T>().Update(obj);
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
