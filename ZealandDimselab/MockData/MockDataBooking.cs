using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.MockData
{
    public static class MockDataBooking
    {
        public static List<Booking> GetBookings()
        {
            return new List<Booking>()
            {
                new Booking(1, GetItems(), GetUser(), "Booking 1", DateTime.Now, DateTime.Now),
                new Booking(2, GetItems(), GetUser(), "Booking 2", DateTime.Now, DateTime.Now),
                new Booking(3, GetItems(), GetUser(), "Booking 3", DateTime.Now, DateTime.Now)
            };
        }

        private static List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("Drone V2", "Epic Drone version 2"),
                new Item("Raspberry Pie", "Raspberry Pie v500"),
                new Item("Smartphone", "Android Pie")
            };
        }
        private static User GetUser()
        {
            return new User(1, "Steven", "Steven@gmail.com", "Hej1234");
        }
    }
}
