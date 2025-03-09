using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eshop.api.Data;
using eshop.api.ViewModels.Orders;
using eshop.api.Entities;

namespace eshop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;


    [HttpGet]
    public async Task<ActionResult> ListAllOrders()
    {
        var orders = await _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .Select(order => new
            {
                OrderNumber = order.OrderNumber,
                order.Customer.FirstName,
                order.Customer.LastName,
                Products = order.OrderItems.Select(item => new
                {
                    item.Product.ProductName,
                    item.TotalPrice,
                    item.Quantity,
                    LineSum = item.TotalPrice * item.Quantity
                })
            })
            .ToListAsync();

        return Ok(new { success = true, StatusCode = 200, data = orders });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> FindOrder(int id)
    {
        var order = await _context.CustomerOrders
            .Where(o => o.Id == id)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Select(order => new
            {
                order = order.Id,
                CustomerName = order.Customer.FirstName + " " + order.Customer.LastName,
                Products = order.OrderItems.Select(item => new
                {
                    item.Product.ProductName,
                    item.TotalPrice,
                    item.Quantity,
                    LineSum = item.TotalPrice * item.Quantity
                })
            })
            .SingleOrDefaultAsync();

        if (order is null)
        {
            return NotFound(new { success = false, StatusCode = 404, message = $"Order could not be found with the id number: {id}."  });
        }

        return Ok(new { success = true, StatusCode = 200, data = order });
    }


    [HttpPost]
    public async Task<ActionResult> AddOrder(OrderViewModel orderModel)
    {
        var newCustomerOrder = new CustomerOrder
        {
            OrderNumber = orderModel.OrderNumber,
            CustomerId = orderModel.CustomerId,
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>()
        };

        foreach (var product in orderModel.orderItems)
        {
            var prod = await _context.Products.FindAsync(product.ProductId);
            if (prod == null)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = $"Product with ID: {product.ProductId} could not be found." });
            }

            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                TotalPrice = (double)product.TotalPrice * product.Quantity
            };

            newCustomerOrder.OrderItems.Add(orderItem);
        }

        try
        {
            _context.CustomerOrders.Add(newCustomerOrder);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, StatusCode = 201, message = "Order has been made." });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(new { success = false, StatusCode = 500, message = "An error occurred while crating order." });
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderViewModel order)
    {
        var orderToUpdate = await _context.CustomerOrders
        .Where(c => c.Id == id)
        .Include(o => o.OrderItems)
        .SingleOrDefaultAsync();

        if (orderToUpdate is null) return BadRequest($"There is no order with this ordernumber {id}");

        orderToUpdate.OrderDate = order.OrderDate;

        foreach (var item in order.orderItems)
        {
            foreach (var orderItem in orderToUpdate.OrderItems)
            {
                orderItem.TotalPrice = item.TotalPrice;
                orderItem.Quantity = item.Quantity;
            }
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
         await _context.OrderItems.FindAsync(id);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet("SökaOrderNumber/{OrderId}")]
    public async Task<IActionResult> sökaoderNumber(string orderNumber)
    {
        var order = await _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

        if (order == null) return NotFound();
        return Ok(order);
    }
    [HttpGet("SearhByDate/{date}")]
    public async Task<IActionResult> SearchbyDate(DateTime date)
    {
        var orders = await _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.OrderDate.Date == date.Date)
            .ToListAsync();

        return Ok(orders);
    }

}