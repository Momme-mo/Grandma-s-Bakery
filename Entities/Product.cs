namespace eshop.api.Entities;

public class Product
{
  public int Id { get; set; }
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public double PricePerUnit { get; set; }
  public string ItemNumber { get; set; }
  public string BestBeforeDate { get; set; }
  public string ManufacturingDate { get; set; }

  // Navigational property...
  public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
  
}
