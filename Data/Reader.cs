namespace LibraryAPI.WebAPI.Data;

public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public DateTime RegisteredAt { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();
}
