namespace eshop.api.Entities;

public class OrderItem
{
  public int Id { get; set; }
  public int OrderItemId { get; set; }
  public int OrderId { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public double TotalPrice { get; set; }

  // Navigational properties...
  public Product Product { get; set; }
  public CustomerOrder CustomerOrder { get; set; }
}
