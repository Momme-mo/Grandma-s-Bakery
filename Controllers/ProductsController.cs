using eshop.api.Data;
using eshop.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eshopAPI.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet]
       public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Products.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"Product with id: {id} could not be found." });
            }

            return Ok(new { success = true, StatusCode = 200, data = product });
        }

      
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductPrice(int id, double newPrice)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = $"Product with id: {id} could not be found." });
            }

            product.PricePerUnit = newPrice;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }