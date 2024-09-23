using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class RetailerController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public RetailerController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Retailer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Retailer>>> GetRetailers()
    {
        return await _context.Retailers.ToListAsync();
    }

    // GET: api/Retailer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Retailer>> GetRetailer(int id)
    {
        var retailer = await _context.Retailers.FindAsync(id);

        if (retailer == null)
        {
            return NotFound();
        }

        return retailer;
    }

    // PUT: api/Retailer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchRetailer(int id, Retailer retailer)
    {
        if (id != retailer.RetailerID)
        {
            return BadRequest();
        }

        // _context.Entry(retailer).State = EntityState.Modified;
        var existingRetailer = await _context.Retailers.FindAsync(id);
        if (existingRetailer == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(retailer.Name))
        {
            existingRetailer.Name = retailer.Name;
        }
        if (!string.IsNullOrEmpty(retailer.Location))
        {
            existingRetailer.Location = retailer.Location;
        }
        if (!string.IsNullOrEmpty(retailer.ContactInfo))
        {
            existingRetailer.ContactInfo = retailer.ContactInfo;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RetailerExists(id))
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

    // POST: api/Retailer
    [HttpPost]
    public async Task<ActionResult<Retailer>> PostRetailer(Retailer retailer)
    {
        _context.Retailers.Add(retailer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRetailer", new { id = retailer.RetailerID }, retailer);
    }

    // DELETE: api/Retailer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRetailer(int id)
    {
        var retailer = await _context.Retailers.FindAsync(id);
        if (retailer == null)
        {
            return NotFound();
        }

        _context.Retailers.Remove(retailer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RetailerExists(int id)
    {
        return _context.Retailers.Any(e => e.RetailerID == id);
    }
}
