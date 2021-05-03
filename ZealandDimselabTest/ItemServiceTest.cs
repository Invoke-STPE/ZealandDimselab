using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class ItemServiceTest
    {
        private ItemService itemService;
        private IDbService<Item> dbService;
        private Item item;

        [TestInitialize]
        public void InitializeTest()
        {
            dbService = new ItemMockData<Item>();
            itemService = new ItemService(dbService);
            item = new Item("Test item 4", "Test description");
        }

        //AddItemTestCases
        [TestMethod]
        public async Task AddItemAsync_CountAsync()
        {
            var expectedCount = itemService.GetAllItems().Count + 1;

            await itemService.AddItemAsync(item);

            var actualValueDb = (await dbService.GetObjectsAsync()).Count();
            var actualValueList = itemService.GetAllItems().Count;

            Assert.AreEqual(expectedCount, actualValueDb);
            Assert.AreEqual(expectedCount, actualValueList);
        }

        [TestMethod]
        public async Task AddItemAsync_CorrectItemAsync()
        {
            Item item = new Item( "TestItem", "Test Description");

            await itemService.AddItemAsync(item);

            Item databaseItem = (await dbService.GetObjectsAsync()).Last();
            Assert.IsTrue(databaseItem.Equals(item));
        }

        //[TestMethod]
        //[ExpectedException(typeof(DbUpdateException))]
        //// Name of item is too long (over 50 characters). Should throw a DbUpdateException
        //public async Task AddItemAsync_InvalidNameLengthAsync()
        //{
        //    Item item = new Item( new String('a', 51), "Test Description");
        //    await itemService.AddItemAsync(item);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(DbUpdateException))]
        //// Name is null, which is not allowed in the database. Should throw a DbUpdateException
        //public async Task AddItemAsync_NullNameAsync()
        //{
        //    Item item = new Item( null, "Test Description");
        //    await itemService.AddItemAsync(item);
        //}


        //DeleteItemCases
        [TestMethod]
        public async Task DeleteItemAsync_ItemCountAsync()
        {
            await itemService.AddItemAsync(item);

            int expectedCount = (await dbService.GetObjectsAsync()).Count() - 1;
            int id = (await dbService.GetObjectsAsync()).Last().Id;

            await itemService.DeleteItemAsync(id);

            int actualValueDb = (await dbService.GetObjectsAsync()).Count();
            int actualValueList = itemService.GetAllItems().Count;

            Assert.AreEqual(expectedCount, actualValueDb);
            Assert.AreEqual(expectedCount, actualValueList);
        }

        [TestMethod]
        public async Task DeleteItemAsync_InvalidItemAsync()
        {
            int expectedCount = (await dbService.GetObjectsAsync()).Count() - 1;

            await itemService.DeleteItemAsync(1);

            int actualValueDb = (await dbService.GetObjectsAsync()).Count();
            int actualValueList = itemService.GetAllItems().Count;

            // Start count and actual count should not be different, as the object does not exist
            Assert.AreEqual(expectedCount, actualValueDb);
            Assert.AreEqual(expectedCount, actualValueList);
        }

        // UpdateItemCases
        [TestMethod]
        public async Task UpdateItemAsync_ValidUpdateAsync()
        {
            await itemService.AddItemAsync(item);
            int id = (await dbService.GetObjectsAsync()).Last().Id;
            item.Name = "NewName";
            item.Description = "NewDescription";

            await itemService.UpdateItemAsync(id, item);

            Assert.IsTrue(item.Equals(await itemService.GetItemByIdAsync(id)));
        }
    }

    public class ItemMockData<T>: IDbService<T> where T: class
    {
        private DimselabDbContext dbContext;

        public ItemMockData()
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
            dbContext.Items.Add(new Item("Fake Test Item 1", "Fake test description"));
            dbContext.Items.Add(new Item("Fake Test Item 2", "Fake test description"));
            dbContext.Items.Add(new Item("Fake Test Item 3", "Fake test description"));

            dbContext.SaveChangesAsync();
        }
    }
}
