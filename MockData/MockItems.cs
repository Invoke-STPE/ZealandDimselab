using System.Collections.Generic;
using ZealandDimselab.Models;

namespace ZealandDimselab.MockData
{
    public static class MockItems
    {
        public static List<Item> Items { get; set; }

        public static List<Item> GetAllItems()
        {
            Items = new List<Item>() { new Item(1, "Wire", "200cm red wire"), new Item(2, "Arduino", "This is an Arduino"), new Item(3, "Drone", "Super cool drone that can fly a lot") };
            return Items;
        }

    }
}