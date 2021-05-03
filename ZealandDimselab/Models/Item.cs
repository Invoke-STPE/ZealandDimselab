﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZealandDimselab.Models
{
    public class Item
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required][MaxLength(50)] public string Name { get; set; }
        [Required][MaxLength(500)] public string Description { get; set; }
        public List<Category> ItemCategories { get; set; }

        public Item()
        {
        }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public bool Equals(Item item)
        {
            return this.Id == item.Id && this.Name == item.Name && this.Description == item.Description;
        }
    }
}