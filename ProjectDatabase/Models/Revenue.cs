using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Models
{
    public class Revenue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public int store_id { get; set; }

        public Store ? Store { get; set; }

        
    }
}
