
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Thiết lập tự động tăng cho ID
        public int id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100)]
        public string username { get; set; } = String.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(100)]
        public string password { get; set;} = String.Empty;
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string name { get; set;} = String.Empty;
        [Required]
        [MaxLength(11)]
        public string phone { get; set; } = String.Empty;
        [Required]
        public int role_id { get; set; }
        public Role ? Role { get; set; }
        public int ? store_id { get; set; }
        public Store? Store { get; set; }
        public ICollection<Order> ? Orders { get; set; }
    }
}
