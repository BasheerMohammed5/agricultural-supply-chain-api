using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class FarmerController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public FarmerController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Farmer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Farmer>>> GetFarmers()
    {
        return await _context.Farmers.ToListAsync();
    }

    // GET: api/Farmer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Farmer>> GetFarmer(int id)
    {
        var farmer = await _context.Farmers.FindAsync(id);

        if (farmer == null)
        {
            return NotFound();
        }

        return farmer;
    }

    // PUT: api/Farmer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchFarmer(int id, Farmer farmer)
    {
        if (id != farmer.FarmerID)
        {
            return BadRequest();
        }

        //_context.Entry(farmer).State = EntityState.Modified;

        var existingFarmer = await _context.Farmers.FindAsync(id);
        if (existingFarmer == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(farmer.Name))
        {
            existingFarmer.Name = farmer.Name;
        }
        if (!string.IsNullOrEmpty(farmer.Location))
        {
            existingFarmer.Location = farmer.Location;
        }
        if (!string.IsNullOrEmpty(farmer.ContactInfo))
        {
            existingFarmer.ContactInfo = farmer.ContactInfo;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FarmerExists(id))
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

    // POST: api/Farmer
    [HttpPost]
    public async Task<ActionResult<Farmer>> PostFarmer(Farmer farmer)
    {
        _context.Farmers.Add(farmer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFarmer", new { id = farmer.FarmerID }, farmer);
    }

    // DELETE: api/Farmer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFarmer(int id)
    {
        var farmer = await _context.Farmers.FindAsync(id);
        if (farmer == null)
        {
            return NotFound();
        }

        _context.Farmers.Remove(farmer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FarmerExists(int id)
    {
        return _context.Farmers.Any(e => e.FarmerID == id);
    }
}
