using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public decimal? total_price { get; set; } = 0;

        [Required]
        public bool status_id { get; set; }

        [MaxLength(1000)]
        public string? note { get; set; }

        public int create_by { get; set; }
        public User? User { get; set; }

        [Required]
        public int store_id { get; set; }
        public Store? Store { get; set; }

        public DateTime create_at { get; set; } = DateTime.UtcNow;


        public DateTime ? update_at { get; set; } = DateTime.UtcNow;


        [Required]
        public int type { get; set; }
        public Order_type? Order_type { get; set; }

        public int? customer_id { get; set; }
        public Customer? Customer { get; set; }

        public decimal? discount_membership { get; set; } = 0;

        public decimal? final_price { get; set; } = 0;

        public virtual ICollection<Orderline>? Orderlines { get; set; }
    }
}
