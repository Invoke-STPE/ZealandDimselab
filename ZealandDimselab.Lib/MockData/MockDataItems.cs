using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Lib.Models;
namespace ZealandDimselab.Lib.MockData
{
    public class MockDataItems
    {
        private List<Item> _items = new List<Item>();

        public MockDataItems()
        {
            _items.AddRange(GetDrones());
            _items.AddRange(GetCables());
            _items.AddRange(GetRaspberryPies());
        }
        public Task AddItemAsync(Item item)
        {
            return Task.Run(() => {
                _items.Add(item);
                });
        }

        public Task DeleteItemAsync(int id)
        {
            Item item = _items.FirstOrDefault(x => x.Id == id);
            return Task.Run(() => {
                _items.Remove(item);
                });
        }

        public Task<IEnumerable<Item>> GetAllItems()
        {
            
            return Task.FromResult((IEnumerable<Item>)_items);
        }

        public Task<List<Item>> GetAllItemsWithCategoriesAsync()
        {
            return Task.FromResult(_items);
        }

        public Task<Item> GetItemByIdAsync(int id)
        {
            return Task.FromResult(_items.First(i => i.CategoryId == id));
        }

        public Task<List<Item>> GetItemsWithCategoryIdAsync(int id)
        {
            return Task.FromResult(_items.FindAll(i => i.CategoryId == id));
        }

        public Task<Item> GetItemWithCategoriesAsync(int id)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public Task ItemStockUpdateAsync(Item item, int bookedQuantity)
        {
            return null;
        }

        public Task UpdateItemAsync(int id, Item item)
        {
            return null;
        }


        private List<Item> GetDrones()
        {
            return new List<Item>()
            {
                new Item{ Id = 1, Name = "DJI Mini 2", Description = "Lille, holder op til 31 minutter I luften.", ImageName = "dji-mini-2.jpg", CategoryId = 1, Quantity = 1, Stock = 1, Category = new Category { CategoryId = 1, CategoryName = "droner", ImageName = null } },
                new Item{ Id = 2, Name = "DJI Mavic 3", Description = "Lavet til billede optagelser, holder op til 41 minutter i luften", ImageName = "dji-mavic-3.jpg", CategoryId = 1, Quantity = 2, Stock = 1, Category = new Category { CategoryId = 1, CategoryName = "droner", ImageName = null } },
                new Item{ Id = 3, Name = "DJI Mini 3 Pro", Description = "Pro version af mini 2, mere advanceret kamera.", ImageName = "dji-mini-2-pro.jpg", CategoryId = 1, Quantity = 1, Stock = 0, Category = new Category { CategoryId = 1, CategoryName = "droner", ImageName = null } },

            };
        }
        private List<Item> GetCables()
        {
            return new List<Item>()
            {
                new Item{ Id = 1, Name = "Netværkskabel - Cat 5e U/UTP - Hvid - 3m", Description = "Båndbredde: 100 MHz, Hastighed: 1 Gbit/s.", ImageName = "cat-5.png", CategoryId = 2, Quantity = 10, Stock = 5, Category = new Category { CategoryId = 2, CategoryName = "Kabler", ImageName = null } },
                new Item{ Id = 2, Name = "DisplayPort kabel - 4K - 1 m", Description = "Dette Displyport kabel er guldbelagt og understøtter 4K opløsning.", ImageName = "displayPort.png", CategoryId = 2, Quantity = 5, Stock = 1, Category = new Category { CategoryId = 2, CategoryName = "Kabler", ImageName = null } },
                new Item{ Id = 3, Name = "Standard 230V netkabel med dansk jord - 3 m", Description = "Standard 230V lysnetkabel, der passer til de fleste computere, scannere, monitorer.", ImageName = "strømkable.png", CategoryId = 2, Quantity = 100, Stock = 150, Category = new Category { CategoryId = 2, CategoryName = "Kabler", ImageName = null } },

            };
        }
        private List<Item> GetRaspberryPies()
        {
            return new List<Item>()
            {
                new Item{ Id = 1, Name = "Raspberry Pi 4 Model B – 8 GB", Description = "Broadcom BCM2711, quad-core Cortex-A72 (ARM v8) 64-bit SoC @ 1.5GHz", ImageName = "raspberry-pi-4-model-b-8-gb-hero-2.png", CategoryId = 3, Quantity = 5, Stock = 5, Category = new Category { CategoryId = 2, CategoryName = "Raspberry Pi", ImageName = null } },
                new Item{ Id = 2, Name = "Okdo Raspberry Pi 4 Model B startkit 4 GB", Description = "Dette eksklusive Raspberry Pi 4-startsæt med otte dele fra OKdo indeholder alt (og vi mener alt) det, du skal bruge for at komme i gang med Raspberry Pi ", ImageName = "Okdo.png", CategoryId = 3, Quantity = 1, Stock = 1, Category = new Category { CategoryId = 2, CategoryName = "Raspberry Pi", ImageName = null } },
                new Item{ Id = 3, Name = "RS PRO Sort Aluminium Raspberry Pi-kabinet", Description = "RS Pro er stolt over at kunne præsentere denne fremragende løsning til Raspberry Pi 4.", ImageName = "rs-pro.png", CategoryId = 3, Quantity = 5, Stock = 4, Category = new Category { CategoryId = 2, CategoryName = "Raspberry Pi", ImageName = null } },

            };
        }
    }
}
