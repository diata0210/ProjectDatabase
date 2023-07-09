using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Orderline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int product_id { get; set; }
        public Product? Product { get; set; }

        public int quantity { get; set; }
        public int order_id { get; set; }
        public Order? Order { get; set; }
    }
}
