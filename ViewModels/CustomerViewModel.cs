namespace eshop.api;

public class CustomerViewModel
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string AddressType { get; set; }
}