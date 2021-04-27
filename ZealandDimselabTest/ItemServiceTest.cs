using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class ItemServiceTest
    {
        private ItemService itemService;
        private GenericDbService<Item> dbService;
        private Item item;

        [TestInitialize]
        public void InitializeTest()
        {
            itemService = new ItemService(new GenericDbService<Item>());
            dbService = new GenericDbService<Item>();
            item = new Item(0, "TestItem", "Test description");
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

            await dbService.DeleteObjectAsync(item);
        }

        [TestMethod]
        public async Task AddItemAsync_CorrectItemAsync()
        {
            Item item = new Item(0, "TestItem", "Test Description");

            await itemService.AddItemAsync(item);

            Item databaseItem = (await dbService.GetObjectsAsync()).Last();
            Assert.IsTrue(databaseItem.Equals(item));

            await dbService.DeleteObjectAsync(item);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        // Name of item is too long (over 50 characters). Should throw a DbUpdateException
        public async Task AddItemAsync_InvalidNameLengthAsync()
        {
            Item item = new Item(0, new String('a', 51), "Test Description");
            await itemService.AddItemAsync(item);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        // Name is null, which is not allowed in the database. Should throw a DbUpdateException
        public async Task AddItemAsync_NullNameAsync()

        {
            Item item = new Item(0, null, "Test Description");
            await itemService.AddItemAsync(item);
        }


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
            int expectedCount = (await dbService.GetObjectsAsync()).Count();

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

            await itemService.DeleteItemAsync(id);
        }
    }

}
