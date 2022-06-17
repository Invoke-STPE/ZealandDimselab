using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;


namespace ZealandDimselab.Pages.Items
{
    public class DeleteItemModel : PageModel
    {
        
        public ItemDto Item { get; set; }
        public List<ItemDto> Items { get; set; }
        [BindProperty] public int CategoryId { get; set; }

        private readonly IHttpClientItem _httpClientItem;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteItemModel(IHttpClientItem httpClientItem, IWebHostEnvironment webHostEnvironment)
        {
            
            _httpClientItem = httpClientItem;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            Items = await _httpClientItem.GetAllItemsWithCategoriesAsync();
            CategoryId = 0;
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategory(int id, int category)
        {
            if (category == 0) return RedirectToPage("DeleteItem", new { id });

            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            Items = await _httpClientItem.GetItemsWithCategoryIdAsync(category);
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var image = (await _httpClientItem.GetItemByIdAsync(id)).ImageName;
            if (!String.IsNullOrEmpty(image))
            {
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", image);
                try
                {
                    System.IO.File.Delete(file);
                }
                catch
                {
                }
            }


            await _httpClientItem.DeleteItemAsync(id);
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    
    }
}
