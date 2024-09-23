using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class QualityEmployeeController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public QualityEmployeeController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/QualityEmployee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QualityEmployee>>> GetQualityEmployees()
    {
        return await _context.QualityEmployees.ToListAsync();
    }

    // GET: api/QualityEmployee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QualityEmployee>> GetQualityEmployee(int id)
    {
        var qualityEmployee = await _context.QualityEmployees.FindAsync(id);

        if (qualityEmployee == null)
        {
            return NotFound();
        }

        return qualityEmployee;
    }

    // PUT: api/QualityEmployee/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchQualityEmployee(int id, QualityEmployee qualityEmployee)
    {
        if (id != qualityEmployee.QualityEmployeeID)
        {
            return BadRequest();
        }

        //_context.Entry(qualityEmployee).State = EntityState.Modified;
        var existingQualityEmployee  = await _context.QualityEmployees.FindAsync(id);
        if (existingQualityEmployee == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(qualityEmployee.Name))
        {
            existingQualityEmployee.Name = qualityEmployee.Name;
        }
        if (!string.IsNullOrEmpty(qualityEmployee.ContactInfo))
        {
            existingQualityEmployee.ContactInfo = qualityEmployee.ContactInfo;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!QualityEmployeeExists(id))
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

    // POST: api/QualityEmployee
    [HttpPost]
    public async Task<ActionResult<QualityEmployee>> PostQualityEmployee(QualityEmployee qualityEmployee)
    {
        _context.QualityEmployees.Add(qualityEmployee);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetQualityEmployee", new { id = qualityEmployee.QualityEmployeeID }, qualityEmployee);
    }

    // DELETE: api/QualityEmployee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQualityEmployee(int id)
    {
        var qualityEmployee = await _context.QualityEmployees.FindAsync(id);
        if (qualityEmployee == null)
        {
            return NotFound();
        }

        _context.QualityEmployees.Remove(qualityEmployee);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool QualityEmployeeExists(int id)
    {
        return _context.QualityEmployees.Any(e => e.QualityEmployeeID == id);
    }
}
