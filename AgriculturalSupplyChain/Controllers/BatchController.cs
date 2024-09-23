using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class BatchController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public BatchController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Batch
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Batch>>> GetBatches()
    {
        return await _context.Batches.ToListAsync();
    }

    // GET: api/Batch/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Batch>> GetBatch(int id)
    {
        var batch = await _context.Batches.FindAsync(id);

        if (batch == null)
        {
            return NotFound();
        }

        return batch;
    }

    // PUT: api/Batch/5
    // PUT: api/Batch/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchBatch(int id, Batch batch)
    {
        // تحقق من تطابق ID
        if (id != batch.BatchID)
        {
            return BadRequest();
        }

        var existingBatch = await _context.Batches.FindAsync(id);
        if (existingBatch == null)
        {
            return NotFound();
        }

        if (batch.BatchID > 0)
        {
            existingBatch.BatchID = batch.BatchID;
        }
        if (batch.FarmerID > 0)
        {
            existingBatch.FarmerID = batch.FarmerID;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(batch.HarvestDate)))
        {
            existingBatch.HarvestDate = batch.HarvestDate;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(batch.ExpiryDate)))
        {
            existingBatch.ExpiryDate = batch.ExpiryDate;
        }
        if (!string.IsNullOrEmpty(batch.QualityCertification))
        {
            existingBatch.QualityCertification = batch.QualityCertification;
        }
        if (!string.IsNullOrEmpty(batch.Location))
        {
            existingBatch.Location = batch.Location;
        }
        // أضف الحقول الأخرى التي تريد تحديثها بنفس الطريقة

        // حفظ التغييرات
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BatchExists(id))
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


    // POST: api/Batch
    [HttpPost]
    public async Task<ActionResult<Batch>> PostBatch(Batch batch)
    {
        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBatch", new { id = batch.BatchID }, batch);
    }

    // DELETE: api/Batch/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBatch(int id)
    {
        var batch = await _context.Batches.FindAsync(id);
        if (batch == null)
        {
            return NotFound();
        }

        _context.Batches.Remove(batch);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BatchExists(int id)
    {
        return _context.Batches.Any(e => e.BatchID == id);
    }
}
