using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZealandDimselab.Models
{
    public class Item
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] [StringLength(100)] public string Name { get; set; }
        [Required] [StringLength(1000)] public string Description { get; set; }

        public Item()
        {
        }

        public Item(int id, string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}