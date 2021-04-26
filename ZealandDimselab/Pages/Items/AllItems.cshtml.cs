using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Items
{
    public class AllItemsModel : PageModel
    {
        public List<Models.Item> Items { get; set; }
        private ItemService _itemService;

        public AllItemsModel(ItemService itemService)
        {
            _itemService = itemService;
            Items = _itemService.GetAllItems();
        }

        public void OnGet()
        {
        }
    }
}
