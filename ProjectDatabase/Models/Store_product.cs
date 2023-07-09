using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Store_product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Thiết lập tự động tăng cho ID

        public int id { get; set; }
        public int store_id { get; set; }
        public Store? Store { get; set; }
        public int product_id { get; set; }
        public Product? Product { get; set; }
        private int _quantity = 0;

        public int quantity { 
            get { return _quantity; }
            set { 
                if(value < 0)
                {
                    throw new InvalidOperationException("Số lượng không hợp lệ.");
                }
                _quantity = value;
            }
        }
        
    }
}
