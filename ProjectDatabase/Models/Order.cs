using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Order
    {
        [Key]
        [Required]
        [MaxLength(8)]
        public string ? id { get; set; }
        [Required]
        public decimal total_price { get; set; }
        [Required]
        [StringLength(1000)]
        public string ? note { get; set; }
        [MaxLength(8)]
        [ForeignKey("User")]
        public string ? create_by { get; set; }
        public User ? User { get; set; }
        [ForeignKey("Store")]
        [Required]
        [MaxLength(8)]
        public string ? store_id { get; set; }
        public Store ? Store { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        [ForeignKey("Order_type")]
        [MaxLength(8)]
        [Required]
        public string ? type { get; set; }
        public Order_type ? Order_type { get; set; }

        [MaxLength(8)]
        [ForeignKey("Customer")]
        public string? customer_id { get; set; }
        public Customer? Customer { get; set; }
        [Required]
        public decimal discount_membership { get; set; }
        [Required]
        public decimal final_price { get; set; }
        [Required]
        
        public ICollection<Orderline> ? Orderlines { get; set; }


    }
}
