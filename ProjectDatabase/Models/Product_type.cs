using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Product_type
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string? name { get; set; }
        [MaxLength(1000)]
        public string? description { get; set; }

        public ICollection<Product> ? Products { get; set; }
    }
}
