using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Store
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string name { get; set; } = string.Empty;
        [Required]
        [StringLength(30)]
        public string description { get; set; } = string.Empty;
        [Required]
        [MaxLength(8)]
        [ForeignKey("Province")]
        public string ? province_id { get; set; }
        public Province ? Province { get; set; }

        [Required]
        [MaxLength(8)]
        [ForeignKey("District")]
        public string? district_id { get; set; }
        public District? District { get; set; }

        public ICollection<User> ? User { get; set; }
        public ICollection<Store_product> ? Store_product { get; set; }

        public ICollection<Order> ? Order { get; set; }
    }
}
