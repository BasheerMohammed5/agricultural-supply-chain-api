using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;
using System.Linq;

namespace AgriculturalSupplyChain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly AgriculturalSupplyChainDbContext _context;

        public StorageController(AgriculturalSupplyChainDbContext context)
        {
            _context = context;
        }

        // GET: api/Storage
        [HttpGet]
        public async Task<IActionResult> GetStorages()
        {
            var storages = await _context.Storage.ToListAsync();
            return Ok(storages);
        }

        // GET: api/Storage/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorage(int id)
        {
            var storage = await _context.Storage.FindAsync(id);

            if (storage == null)
                return NotFound();

            return Ok(storage);
        }

        // POST: api/Storage
        [HttpPost]
        public async Task<IActionResult> CreateStorage([FromBody] Storage storage)
        {
            _context.Storage.Add(storage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStorage), new { id = storage.StorageID }, storage);
        }

        // PUT: api/Storage/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStorage(int id, [FromBody] Storage updatedStorage)
        {
            var storage = await _context.Storage.FindAsync(id);

            if (storage == null)
                return NotFound();

            // تحديث الحقول المحددة فقط
            if (updatedStorage.BatchID != 0)
                storage.BatchID = updatedStorage.BatchID;

            if (updatedStorage.StorageDate != default(DateTime))
                storage.StorageDate = updatedStorage.StorageDate;

            if (!string.IsNullOrEmpty(updatedStorage.StorageLocation))
                storage.StorageLocation = updatedStorage.StorageLocation;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Storage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage(int id)
        {
            var storage = await _context.Storage.FindAsync(id);

            if (storage == null)
                return NotFound();

            _context.Storage.Remove(storage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageExists(int id)
        {
            return _context.Storage.Any(e => e.StorageID == id);
        }
    }
}
