using LibraryAPI.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadersController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public ReadersController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/readers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
    {
        return await _context.Readers.ToListAsync();
    }

    // GET: api/readers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);

        if (reader == null)
        {
            return NotFound();
        }

        return reader;
    }

    // POST: api/readers
    [HttpPost]
    public async Task<ActionResult<Reader>> PostReader(Reader reader)
    {
        reader.RegisteredAt = DateTime.UtcNow;
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, reader);
    }

    // PUT: api/readers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReader(int id, Reader reader)
    {
        if (id != reader.Id)
        {
            return BadRequest();
        }

        _context.Entry(reader).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Readers.Any(e => e.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/readers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);

        if (reader == null)
        {
            return NotFound();
        }

        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
