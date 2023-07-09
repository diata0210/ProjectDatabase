using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Thiết lập tự động tăng cho ID

        public int id { get; set; } 
        [Required]
        [MaxLength(1000)]
        public string name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string ? description { get; set; } 
        public int  province_id { get; set; }
        public Province ? Province { get; set; }

        public int district_id { get; set; }
        public District? District { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? address { get; set; }
        public ICollection<User> ? Users { get; set; }
        public ICollection<Store_product> ? Store_products { get; set; }

        public ICollection<Order> ? Orders { get; set; }
        public ICollection<Revenue> ? Revenues { get; set; }
    }
}
