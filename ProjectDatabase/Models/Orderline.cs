using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Orderline
    {
        [Key]
        [Required]
        [MaxLength(8)]
        public string ? id { get; set; }
        [ForeignKey("Product")]
        [Required]
        [MaxLength(8)] 
        public string ? product_id { get; set; }
        public Product ? Product { get; set; }
        public int quantity { get; set; }
        [ForeignKey("Order")]
        [Required]
        [MaxLength(8)]

        public string ? order_id { get; set; }
        public Order Order { get; set; }


    }
}
