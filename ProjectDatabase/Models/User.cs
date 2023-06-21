
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class User
    {
        [Key]
        [MaxLength(8)]
        public string id { get; set; } = String.Empty;
        [Required(ErrorMessage = "Username is required.")]
        [SQLite.Unique]
        [StringLength(30)]
        public string username { get; set; } = String.Empty;
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20)]
        public string password { get; set;} = String.Empty;
        [Required]
        [StringLength(30)]
        public string name { get; set;} = String.Empty;
        [Required]
        [MaxLength(11)]
        public string phone { get; set; } = String.Empty;
        [ForeignKey("Role")]
        [MaxLength(8)]
        [Required]
        public string role_id { get; set; } = String.Empty;
        public Role ? Role { get; set; }

        [ForeignKey("Store")]
        [MaxLength(8)]
        public string ? store_id { get; set; }
        public Store? Store { get; set; }
        public ICollection<Order> ? Order { get; set; }
    }
}
