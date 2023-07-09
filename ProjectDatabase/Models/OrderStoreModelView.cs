namespace ProjectDatabase.Models
{
    public class OrderStoreModelView
    {
        public List<Order> ? Orders { get; set; }
        public List<Store> ? Stores { get; set; }
        public List<Orderline> ? Orderlines { get; set; }

    }
}
