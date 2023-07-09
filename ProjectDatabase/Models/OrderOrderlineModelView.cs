using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectDatabase.Models
{
    public class OrderOrderlineModelView
    {
        public Order ? Order { get; set; }
        public List<Orderline> ? Orderline { get; set; }
        public SelectList ? Store_product { get; set; }
        public SelectList ?Store { get; set; }

    }
}
