using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class ShipmentController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public ShipmentController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Shipment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
    {
        return await _context.Shipments.ToListAsync();
    }

    // GET: api/Shipment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Shipment>> GetShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);

        if (shipment == null)
        {
            return NotFound();
        }

        return shipment;
    }

    // PUT: api/Shipment/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchShipment(int id, Shipment shipment)
    {
        if (id != shipment.ShipmentID)
        {
            return BadRequest();
        }

        //_context.Entry(shipment).State = EntityState.Modified;
        var existingShipment = await _context.Shipments.FindAsync(id);
        if (existingShipment == null)
        {
            return NotFound();
        }

        if (shipment.BatchID > 0)
        {
            existingShipment.BatchID = shipment.BatchID;
        }
        if (shipment.SupID > 0)
        {
            existingShipment.SupID = shipment.SupID;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(shipment.ShipmentDate)))
        {
            existingShipment.ShipmentDate = shipment.ShipmentDate;
        }
        if (!string.IsNullOrEmpty(shipment.Status))
        {
            existingShipment.Status = shipment.Status;
        }
        if (!string.IsNullOrEmpty(shipment.GPSLocation))
        {
            existingShipment.GPSLocation = shipment.GPSLocation;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShipmentExists(id))
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

    // POST: api/Shipment
    [HttpPost]
    public async Task<ActionResult<Shipment>> PostShipment(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShipment", new { id = shipment.SupID }, shipment);
    }

    // DELETE: api/Shipment/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShipmentExists(int id)
    {
        return _context.Shipments.Any(e => e.ShipmentID == id);
    }
}
