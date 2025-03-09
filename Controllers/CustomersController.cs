using eshop.api.Data;
using eshop.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eshop.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(DataContext context) : ControllerBase
{
  private readonly DataContext _context = context;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
  {
    return await _context.Customers.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Customer>> GetCustomer(int id)
  {
    var customer = await _context.Customers
      .Include(c => c.CustomerOrders)
      .ThenInclude(o => o.OrderItems)
      .ThenInclude(oi => oi.Product)
      .FirstOrDefaultAsync(c => c.CustomerId == id);

    if (customer == null)
    {
      return NotFound(new { success = false, message = "Customer not found with the id number: {id}." });
    }

    return Ok(new { success = true, StatusCode = 200, data = customer });
  }

  [HttpPost()]
  public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
  {
    _context.Customers.Add(customer);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> ChangeCustomer(int id, string ChangeCustomer)
  {
    var customer = await _context.Customers.FindAsync(id);
    if (customer == null)
    {
      return NotFound(new { success = true, StatusCode = 404, message = $"Customer not found with the id number: {id}."});
    }

    customer.FirstName = ChangeCustomer;
    customer.LastName = ChangeCustomer;
    await _context.SaveChangesAsync();

    return NoContent();
  }
}