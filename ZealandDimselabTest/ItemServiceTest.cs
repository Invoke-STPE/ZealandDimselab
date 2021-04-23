using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZealandDimselab.Models;
using ZealandDimselab.Repository;
using ZealandDimselab.Services;

namespace ZealandDimselabTest
{
    [TestClass]
    public class ItemServiceTest
    {
        private ItemMockData items;
        private ItemService itemService;
        [TestInitialize]
        public void InitializeTest()
        {
            items = new ItemMockData();
            itemService = new ItemService(items);
        }

        //AddItemTestCases
        [TestMethod]
        public void AddItemAsync_AddItem_IncrementsCount()
        {
            //Arrange
            Item item = new Item(4, "TestItem4", "Test description 4");
            int expectedCount = 4;
            itemService.AddItemAsync(item);

            //Act
            int actualCount = itemService.GetAllItems().Count;

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemAsync_SameId_ThrowKeyAlreadyExists()
        {
            //Arrange
            Item item = new Item(3, "FakeTestItem3", "Fake Test description 3");
            Exception exception = null;

            itemService.AddItemAsync(item);
            itemService.AddItemAsync(item);
        }

    }

    public class ItemMockData: IRepository<Item>
    {
        public List<Item> Items { get; set; }
        public ItemMockData()
        {
            Items = new List<Item>(){ new Item(1, "TestItem1", "Test description 1"), new Item(2, "TestItem2", "Test description 2"), new Item(3, "TestItem3", "Test description 3")};
        }

        public List<Item> GetAllAsync()
        {
            return Items;
        }

        public Task<Item> GetObjectByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateObjectAsync(Item entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteObjectAsync(Item entity)
        {
            throw new System.NotImplementedException();
        }

        public Task AddObjectAsync(Item entity)
        {
            return null;
        }
    }
}
