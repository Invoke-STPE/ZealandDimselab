using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ZealandDimselab.WPF.DtoModels;

namespace ZealandDimselab.WPF.Cart
{
    public static class Cart
    {
        private static List<ItemDto> _items = new List<ItemDto>();


        public static void AddToCart(ItemDto item)
        {
            _items.Add(item);
        }

        internal static int GetCartCount()
        {
            return _items.Count;
        }

        internal static List<ItemDto> GetCartItems()
        {
            return _items;
        }
    }
}
