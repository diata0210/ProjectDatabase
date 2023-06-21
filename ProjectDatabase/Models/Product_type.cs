﻿using System.ComponentModel.DataAnnotations;

namespace ProjectDatabase.Models
{
    public class Product_type
    {
        
        [Key]
        [Required]
        [MaxLength(8)]
        public string? id { get; set; }
        [Required]
        [StringLength(100)]
        public string? name { get; set; }
        [StringLength(1000)]
        public string? description { get; set; }

        public ICollection<Product> ? Product { get; set; }
    }
}
