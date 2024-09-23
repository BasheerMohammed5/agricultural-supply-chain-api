using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public ProductController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchProduct(int id, Product product)
    {
        if (id != product.ProductID)
        {
            return BadRequest();
        }

        //_context.Entry(product).State = EntityState.Modified;
        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(product.Name))
        {
            existingProduct.Name = product.Name;
        }
        if (!string.IsNullOrEmpty(product.Description))
        {
            existingProduct.Description = product.Description;
        }
        if (!string.IsNullOrEmpty(product.Category))
        {
            existingProduct.Category = product.Category;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductID == id);
    }
}
