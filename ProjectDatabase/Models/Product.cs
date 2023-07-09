using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectDatabase.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ? name { get; set; }
        [Required]
        public Decimal price { get; set; }
        [MaxLength(1000)]
        public string? description { get; set; }
        public int type { get; set; }
        public Product_type ? Product_type { get; set; }
        public DateTime ? delete_at { get; set; }
        public ICollection<Store_product>? Store_products { get; set; }
        public ICollection<Orderline> ? Orderlines { get; set; }
    }
}
