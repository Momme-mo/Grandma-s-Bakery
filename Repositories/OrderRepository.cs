using eshop.api.Data;
using eshop.api.Entities;
using eshop.api.Interfaces;
using eshop.api.ViewModels.Orders;

namespace eshop.api.Repositories
{
    public class OrderRepository(DataContext context) : IOrderRepository
    {
        private readonly DataContext _context = context;

        public Task<OrderItem> Find(OrderViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}