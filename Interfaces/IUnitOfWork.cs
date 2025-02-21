using eshop.api.Interfaces;

namespace eshop.api;

public interface IUnitOfWork
{
  ICustomerRepository CustomerRepository { get; }
  IAddressRepository AddressRepository { get; }
  IOrderRepository OrderRepository { get; }

  Task<bool> Complete();
  bool HasChanges();
}