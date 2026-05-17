using LibraryAPI.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public LoansController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/loans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetLoans()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Reader)
            .Select(l => new
            {
                l.Id,
                l.BookId,
                BookTitle = l.Book != null ? l.Book.Title : null,
                l.ReaderId,
                ReaderName = l.Reader != null
                    ? l.Reader.FirstName + " " + l.Reader.LastName
                    : null,
                l.LoanDate,
                l.DueDate,
                l.ReturnDate,
                IsOverdue = l.ReturnDate == null && l.DueDate < DateTime.UtcNow
            })
            .ToListAsync();
    }

    // GET: api/loans/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetLoan(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Reader)
            .Where(l => l.Id == id)
            .Select(l => new
            {
                l.Id,
                l.BookId,
                BookTitle = l.Book != null ? l.Book.Title : null,
                l.ReaderId,
                ReaderName = l.Reader != null
                    ? l.Reader.FirstName + " " + l.Reader.LastName
                    : null,
                l.LoanDate,
                l.DueDate,
                l.ReturnDate,
                IsOverdue = l.ReturnDate == null && l.DueDate < DateTime.UtcNow
            })
            .FirstOrDefaultAsync();

        if (loan == null)
        {
            return NotFound();
        }

        return loan;
    }

    // POST: api/loans
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        loan.LoanDate = DateTime.UtcNow;
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
    }

    // PUT: api/loans/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLoan(int id, Loan loan)
    {
        if (id != loan.Id)
        {
            return BadRequest();
        }

        _context.Entry(loan).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Loans.Any(e => e.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // PATCH: api/loans/5/return — Повернення книги
    [HttpPatch("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var loan = await _context.Loans.FindAsync(id);

        if (loan == null)
        {
            return NotFound();
        }

        if (loan.ReturnDate != null)
        {
            return BadRequest(new { message = "This book has already been returned." });
        }

        loan.ReturnDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/loans/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);

        if (loan == null)
        {
            return NotFound();
        }

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
