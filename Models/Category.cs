namespace LibraryAPI.WebAPI.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
}
