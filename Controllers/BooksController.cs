using LibraryAPI.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public BooksController(LibraryDbContext context)
    {
        _context = context;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetBooks()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Select(b => new
            {
                b.Id,
                b.Title,
                b.ISBN,
                b.PublishedYear,
                b.TotalCopies,
                b.CategoryId,
                CategoryName = b.Category != null ? b.Category.Name : null,
                b.AuthorId,
                AuthorName = b.Author != null
                    ? b.Author.FirstName + " " + b.Author.LastName
                    : null
            })
            .ToListAsync();
    }

    // GET: api/books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetBook(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Where(b => b.Id == id)
            .Select(b => new
            {
                b.Id,
                b.Title,
                b.ISBN,
                b.PublishedYear,
                b.TotalCopies,
                b.CategoryId,
                CategoryName = b.Category != null ? b.Category.Name : null,
                b.AuthorId,
                AuthorName = b.Author != null
                    ? b.Author.FirstName + " " + b.Author.LastName
                    : null
            })
            .FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Books.Any(e => e.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
