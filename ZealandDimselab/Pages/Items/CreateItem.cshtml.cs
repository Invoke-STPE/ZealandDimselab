using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ZealandDimselab.Lib.Models;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.DTO;

namespace ZealandDimselab.Pages.Items
{
    public class CreateItemModel : PageModel
    {

        [BindProperty] public ItemDto Item { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<CategoryDto> Categories { get; set; }
        [BindProperty] public int CategoryId { get; set; }
        [BindProperty] public IFormFile FileUpload { get; set; }

        private readonly IHttpClientItem _httpClientItem;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpClientCategory _httpClientCategory;

        public CreateItemModel(IHttpClientItem httpClientItem, IWebHostEnvironment whe, IHttpClientCategory httpClientCategory)
        {
            _httpClientItem = httpClientItem;
            _webHostEnvironment = whe;
            _httpClientCategory = httpClientCategory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Items = await _httpClientItem.GetAllItemsWithCategoriesAsync();
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            CategoryId = 0;

            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int category)
        {
            if (category == 0) return RedirectToPage("CreateItem");

            Items = await _httpClientItem.GetItemsWithCategoryIdAsync(category);
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                if (CategoryId == 0) return await OnGetAsync();
                return await OnGetFilterByCategoryAsync(CategoryId);
            }

            await _httpClientItem.AddItemAsync(Item);
            ItemDto item = (await _httpClientItem.GetAllItemsAsync()).Last();

            if (FileUpload != null)
            {
                var fileName = item.Id + "." + FileUpload.ContentType.TrimStart('i','m','a','g','e','/');
                var fileUpload = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", fileName);
                using (var Fs = new FileStream(fileUpload, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(Fs);
                }

                item.ImageName = fileName;
            }

            await _httpClientItem.UpdateItemAsync(item);

            if (CategoryId == 0) return RedirectToPage("AllItems");
            if (Item.CategoryId != CategoryId)
                return RedirectToPage("AllItems", "FilterByCategory", new {category = Item.CategoryId});
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    }
}
