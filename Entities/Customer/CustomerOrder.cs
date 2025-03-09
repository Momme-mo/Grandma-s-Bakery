using System.ComponentModel.DataAnnotations;

namespace eshop.api.Entities;

public class CustomerOrder
{
  [Key]
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public string OrderNumber { get; set; }
  public DateTime OrderDate { get; set; }

  public Customer Customer { get; set; }

  // Navigational property...
  public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
