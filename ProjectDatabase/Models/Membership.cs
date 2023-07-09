using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string ? description { get; set; } = string.Empty;
        [Required]
        public decimal discount_value { get; set; }
        
        public ICollection<Customer> ? Customers { get; set; }


    }
}
