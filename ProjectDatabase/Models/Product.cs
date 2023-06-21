using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectDatabase.Models
{
    public class Product
    {
        [Key]
        [Required]
        [MaxLength(8)]
        public string ? id { get; set; }
        [Required]
        [StringLength(100)]
        public string ? name { get; set; }
        [Required]
        public Decimal ? Price { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        public float discount { get; set; } = 0;
        [Required]
        [StringLength(100)]
        [ForeignKey("Product_type")]
        public string? type { get; set; }
        public Product_type ? Product_type { get; set; }
        public DateTime  deleteAt { get; set; }
        public ICollection<Store_product>? Store_product { get; set; }
        public ICollection<Orderline> ? Orderline { get; set; }
    }
}
