using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Order_type
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ? name { get; set; }
        [MaxLength(1000)]
        public string ? description { get; set; }
        public ICollection<Order> ? Orders { get; set; }
    }
}
