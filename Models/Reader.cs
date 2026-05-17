namespace LibraryAPI.WebAPI.Data;

public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public DateTime RegisteredAt { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();
}
