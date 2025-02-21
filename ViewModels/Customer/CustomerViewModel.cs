using eshop.api.ViewModels.Orders;

namespace eshop.api;

public class CustomerViewModel : CustomerBaseViewModel
{
  public int Id { get; set; }

  public IList<AddressViewModel> Addresses { get; set; }
  public IList<OrderViewModel> Orders { get; set; }

}