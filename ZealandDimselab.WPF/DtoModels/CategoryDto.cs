using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ZealandDimselab.WPF.DtoModels
{
    public class CategoryDto
    {
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }
        [JsonPropertyName("imageName")]
        public string ImageName { get; set; }
    }
}
