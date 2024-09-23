using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgriculturalSupplyChain.Data;
using AgriculturalSupplyChain.Models;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly AgriculturalSupplyChainDbContext _context;

    public FeedbackController(AgriculturalSupplyChainDbContext context)
    {
        _context = context;
    }

    // GET: api/Feedback
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
    {
        return await _context.Feedbacks.ToListAsync();
    }

    // GET: api/Feedback/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Feedback>> GetFeedback(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return feedback;
    }

    // PUT: api/Feedback/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PatchFeedback(int id, Feedback feedback)
    {
        if (id != feedback.FeedbackID)
        {
            return BadRequest();
        }

        // _context.Entry(feedback).State = EntityState.Modified;
        var existingFeedback = await _context.Feedbacks.FindAsync(id);
        if (existingFeedback == null)
        {
            return NotFound();
        }

        if (feedback.BatchID > 0)
        {
            existingFeedback.BatchID = feedback.BatchID;
        }
        if (feedback.ConsID > 0)
        {
            existingFeedback.ConsID = feedback.ConsID;
        }
        if (!string.IsNullOrEmpty(Convert.ToString(feedback.Date)))
        {
            existingFeedback.Date = feedback.Date;
        }
        if (!string.IsNullOrEmpty(feedback.Comments))
        {
            existingFeedback.Comments = feedback.Comments;
        }
        if (feedback.Rating > 0)
        {
            existingFeedback.Rating = feedback.Rating;
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FeedbackExists(id))
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

    // POST: api/Feedback
    [HttpPost]
    public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
    {
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackID }, feedback);
    }

    // DELETE: api/Feedback/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeedback(int id)
    {
        var feedback = await _context.Feedbacks.FindAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FeedbackExists(int id)
    {
        return _context.Feedbacks.Any(e => e.FeedbackID == id);
    }
}
