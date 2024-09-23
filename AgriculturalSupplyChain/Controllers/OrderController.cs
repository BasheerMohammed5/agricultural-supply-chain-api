using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public OrderController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // PUT: api/Order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchOrder(int id, Order order)
    {
        if (id != order.OrderID)
        {
            return BadRequest();
        }

        var existingOrder = await _context.Orders.FindAsync(id);
        if (existingOrder == null)
        {
            return NotFound();
        }
        if (order.BatchID > 0)
        {
            existingOrder.BatchID = order.BatchID;
        }
        if (order.RetID > 0)
        {
            existingOrder.RetID = order.RetID;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(order.OrderDate)))
        {
            existingOrder.OrderDate = order.OrderDate;
        }
        if (!string.IsNullOrEmpty(order.OrderStatus))
        {
            existingOrder.OrderStatus = order.OrderStatus;
        }


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id))
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

    // POST: api/Order
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
    }

    // DELETE: api/Order/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.OrderID == id);
    }
}
