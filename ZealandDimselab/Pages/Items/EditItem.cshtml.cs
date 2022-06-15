using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;


namespace ZealandDimselab.Pages.Items
{
    public class EditItemModel : PageModel
    {
  
        [BindProperty] public ItemDto Item { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<CategoryDto> Categories { get; set; }
        [BindProperty] public int CategoryId { get; set; }
        [BindProperty] public IFormFile FileUpload { get; set; }

        private readonly IHttpClientItem _httpClientItem;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpClientBooking _httpClientBooking;
        private readonly IHttpClientCategory _httpClientCategory;

        public EditItemModel(IHttpClientItem httpClientItem,
                            IWebHostEnvironment whe,
                            IHttpClientBooking httpClientBooking,
                            IHttpClientCategory httpClientCategory)
        {
            _httpClientItem = httpClientItem;
            _webHostEnvironment = whe;
            _httpClientBooking = httpClientBooking;
            _httpClientCategory = httpClientCategory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Items = await _httpClientItem.GetAllItemsWithCategoriesAsync();
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            CategoryId = 0;

            return Page();
        }

        public async Task<IActionResult> OnGetFilterByCategoryAsync(int id, int category)
        {
            if (category == 0) return RedirectToPage("EditItem", new { id });

            Items = await _httpClientItem.GetItemsWithCategoryIdAsync(category);
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
            Item = await _httpClientItem.GetItemWithCategoriesAsync(id);
            CategoryId = category;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (CategoryId == 0) return await OnGetAsync(Item.Id);
                return await OnGetFilterByCategoryAsync(Item.Id, CategoryId);
            }

            if (FileUpload != null)
            {
                var fileName = Item.Id + "." + FileUpload.ContentType.TrimStart('i', 'm', 'a', 'g', 'e', '/');
                var fileUpload = Path.Combine(_webHostEnvironment.WebRootPath, "images/ItemImages", fileName);
                using (var Fs = new FileStream(fileUpload, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(Fs);
                }

                Item.ImageName = fileName;
            }
            else
            {
                Item.ImageName = (await _httpClientItem.GetItemWithCategoriesAsync(Item.Id)).ImageName;
            }

            var bookedItems = await _httpClientBooking.GetAllBookedItemsAsync();
            int matchingId = 0;
            foreach (var bookedItem in bookedItems)
            {
                if (bookedItem.Item.Id == Item.Id && String.Equals(bookedItem.Status, "Not Returned"))
                {
                    matchingId += bookedItem.Quantity;
                }
            }
            Item.Stock = Item.Quantity - matchingId;

            await _httpClientItem.UpdateItemAsync(Item);
            return RedirectToPage("AllItems", "FilterByCategory", new { category = CategoryId });
        }
    }
}
