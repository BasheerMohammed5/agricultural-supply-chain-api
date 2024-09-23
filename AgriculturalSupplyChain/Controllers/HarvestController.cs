using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class HarvestController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public HarvestController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Harvest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Harvest>>> GetHarvests()
    {
        return await _context.Harvests.ToListAsync();
    }

    // GET: api/Harvest/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Harvest>> GetHarvest(int id)
    {
        var harvest = await _context.Harvests.FindAsync(id);

        if (harvest == null)
        {
            return NotFound();
        }

        return harvest;
    }

    // PUT: api/Harvest/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchHarvest(int id, Harvest harvest)
    {
        if (id != harvest.HarvestID)
        {
            return BadRequest();
        }

        //_context.Entry(harvest).State = EntityState.Modified;

        var existingHarvest = await _context.Harvests.FindAsync(id);
        if (existingHarvest == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(Convert.ToString(harvest.HarvestDate)))
        {
            existingHarvest.HarvestDate = harvest.HarvestDate;
        }
        if (harvest.BatchID > 0)
        {
            existingHarvest.BatchID = harvest.BatchID;
        }
        if (harvest.Quantity > 0)
        {
            existingHarvest.Quantity = harvest.Quantity;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HarvestExists(id))
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

    // POST: api/Harvest
    [HttpPost]
    public async Task<ActionResult<Harvest>> PostHarvest(Harvest harvest)
    {
        _context.Harvests.Add(harvest);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHarvest", new { id = harvest.HarvestID }, harvest);
    }

    // DELETE: api/Harvest/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHarvest(int id)
    {
        var harvest = await _context.Harvests.FindAsync(id);
        if (harvest == null)
        {
            return NotFound();
        }

        _context.Harvests.Remove(harvest);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HarvestExists(int id)
    {
        return _context.Harvests.Any(e => e.HarvestID == id);
    }
}
