using eshop.api.Entities;

namespace eshop.api.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItemViewModel> orderItems { get; set; }
    }
}