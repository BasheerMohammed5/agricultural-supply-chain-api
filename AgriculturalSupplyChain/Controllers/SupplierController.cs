﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public SupplierController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Supplier
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
    {
        return await _context.Suppliers.ToListAsync();
    }

    // GET: api/Supplier/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> GetSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier == null)
        {
            return NotFound();
        }

        return supplier;
    }

    // PUT: api/Supplier/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchSupplier(int id, Supplier supplier)
    {
        if (id != supplier.SupplierID)
        {
            return BadRequest();
        }

        // _context.Entry(supplier).State = EntityState.Modified;
        var existingSupplier = await _context.Suppliers.FindAsync(id);
        if (existingSupplier == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(supplier.Name))
        {
            existingSupplier.Name = supplier.Name;
        }
        if (!string.IsNullOrEmpty(supplier.Location))
        {
            existingSupplier.Location = supplier.Location;
        }
        if (!string.IsNullOrEmpty(supplier.ContactInfo))
        {
            existingSupplier.ContactInfo = supplier.ContactInfo;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SupplierExists(id))
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

    // POST: api/Supplier
    [HttpPost]
    public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSupplier", new { id = supplier.SupplierID }, supplier);
    }

    // DELETE: api/Supplier/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SupplierExists(int id)
    {
        return _context.Suppliers.Any(e => e.SupplierID == id);
    }
}
