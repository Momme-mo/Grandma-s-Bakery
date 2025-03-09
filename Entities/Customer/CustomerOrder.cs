using System.ComponentModel.DataAnnotations;

namespace eshop.api.Entities;

public class CustomerOrder
{
  [Key]
  public int Id { get; set; }
  public int OrderId { get; set; }
  public string OrderNumber { get; set; }
  public int CustomerId { get; set; }
  public DateTime OrderDate { get; set; }

  // Navigational property...
  public Customer Customer { get; set; }
  public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
