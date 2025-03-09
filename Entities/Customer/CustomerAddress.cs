namespace eshop.api.Entities;

public class CustomerAddress
{
  public int CustomerId { get; set; }
  public int AddressId { get; set; }

    // [ForeignKey("CustomerId")]
  public Customer Customer { get; set; }
  
    // [ForeignKey("AddressId")]
  public Address Address { get; set; }
}