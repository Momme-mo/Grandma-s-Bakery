namespace eshop.api.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}