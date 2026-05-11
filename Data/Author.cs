namespace LibraryAPI.WebAPI.Data;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
}
