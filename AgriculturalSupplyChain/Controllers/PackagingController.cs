using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class PackagingController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public PackagingController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Packaging
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Packaging>>> GetPackagings()
    {
        return await _context.Packagings.ToListAsync();
    }

    // GET: api/Packaging/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Packaging>> GetPackaging(int id)
    {
        var packaging = await _context.Packagings.FindAsync(id);

        if (packaging == null)
        {
            return NotFound();
        }

        return packaging;
    }

    // PUT: api/Packaging/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchPackaging(int id, Packaging packaging)
    {
        if (id != packaging.PackagingID)
        {
            return BadRequest();
        }

        // _context.Entry(packaging).State = EntityState.Modified;
        var existingPackaging = await _context.Packagings.FindAsync(id);
        if (existingPackaging == null)
        {
            return NotFound();
        }

        if (packaging.BatchID > 0)
        {
            existingPackaging.BatchID = packaging.BatchID;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(packaging.PackagingDate)))
        {
            existingPackaging.PackagingDate = packaging.PackagingDate;
        }
        if (!string.IsNullOrEmpty(packaging.PackagingDetails))
        {
            existingPackaging.PackagingDetails = packaging.PackagingDetails;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PackagingExists(id))
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

    // POST: api/Packaging
    [HttpPost]
    public async Task<ActionResult<Packaging>> PostPackaging(Packaging packaging)
    {
        _context.Packagings.Add(packaging);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPackaging", new { id = packaging.PackagingID }, packaging);
    }

    // DELETE: api/Packaging/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePackaging(int id)
    {
        var packaging = await _context.Packagings.FindAsync(id);
        if (packaging == null)
        {
            return NotFound();
        }

        _context.Packagings.Remove(packaging);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PackagingExists(int id)
    {
        return _context.Packagings.Any(e => e.PackagingID == id);
    }
}
