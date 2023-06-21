using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Customer
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string name { get; set; } = string.Empty;
        [Required]
        [StringLength(11)]
        [ForeignKey("Membership")]
        [MaxLength(8)]
        public string membership_id { get; set; }
        public Membership ? Membership { get; set; }
        public string phone { get;set ; } = string.Empty;
        public DateTime dob { get; set; }
        public ICollection<Order> Order { get; set; }

    }
}
