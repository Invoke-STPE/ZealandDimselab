using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.DTO;
using ZealandDimselab.Helpers.HttpClients;
using ZealandDimselab.Lib.Models;

namespace ZealandDimselab.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientCategory _httpClientCategory;

        public List<CategoryDto> Categories { get; set; }

        public IndexModel(IHttpClientCategory httpClientCategory)
        {
            _httpClientCategory = httpClientCategory;
        }

        public async Task OnGetAsync()
        {
            Categories = await _httpClientCategory.GetAllCategoriesAsync();
        }
    }
}
