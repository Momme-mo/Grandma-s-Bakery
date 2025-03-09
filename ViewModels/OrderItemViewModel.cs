using eshop.api.Entities;

namespace eshop.api.ViewModels.Orders
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
    }
}