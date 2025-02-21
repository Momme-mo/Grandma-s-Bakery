using eshop.api.Entities;
using eshop.api.ViewModels.Orders;

namespace eshop.api.Interfaces;

    public interface IOrderRepository
    {
        public Task<OrderItem> Find(OrderViewModel model);   
    }
