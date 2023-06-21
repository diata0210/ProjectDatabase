using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Store_product
    {
        [Key]
        [Required]
        [MaxLength(8)]
        public string? id { get; set; }
        [Required]
        [ForeignKey("Store")]
        [MaxLength(8)]
        public string ? store_id { get; set; }
        public Store? Store { get; set; }
        [Required]
        [ForeignKey("Product")]
        [MaxLength(8)]
        public string  ? product_id { get; set; }
        public Product? Product { get; set; }
        [Required]
        public int quantity { get; set; } = 0;
        
    }
}
