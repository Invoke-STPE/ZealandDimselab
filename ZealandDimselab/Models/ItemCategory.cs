using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class ItemCategory
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public Item Item { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ItemCategory()
        {

        }
    }
}
