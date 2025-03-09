namespace eshop.api.ViewModels;

public class ProductGetViewModel
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public string ItemNumber { get; set; }
  public double PricePerUnit { get; set; }
  public int Quantity { get; set; }
}
