using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;
namespace ProjectDatabase.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [StringLength(1000)]
        public string name { get; set; } = string.Empty;
        [Required]
        public int membership_id { get; set; } = 1;

        public Membership ? Membership { get; set; }
        [StringLength(11)]
        public string phone { get;set ; } = string.Empty;
        [Column(TypeName = "date")] 
        public DateTime dob { get; set; } 

        public ICollection<Order> ? Orders { get; set; }

    }
}
