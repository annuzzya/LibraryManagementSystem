namespace LibraryAPI.WebAPI.Data;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? ISBN { get; set; }
    public int? PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }

    public virtual Category? Category { get; set; }
    public virtual Author? Author { get; set; }
    public virtual ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();
}
