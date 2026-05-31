namespace LibraryAPI.WebAPI.Data;

public static class DbSeeder
{
    public static void Seed(LibraryDbContext db)
    {
        if (db.Categories.Any()) return;

        var categories = new[]
        {
            new Category { Name = "Fiction",     Description = "Novels and short stories" },
            new Category { Name = "Science",     Description = "Popular science books" },
            new Category { Name = "History",     Description = "Historical works" },
            new Category { Name = "Programming", Description = "Software development and IT" },
            new Category { Name = "Philosophy",  Description = "Philosophy and ethics" }
        };
        db.Categories.AddRange(categories);
        db.SaveChanges();

        var authors = new[]
        {
            new Author { FirstName = "George",  LastName = "Orwell",     Biography = "English novelist, author of 1984." },
            new Author { FirstName = "Frank",   LastName = "Herbert",    Biography = "Author of Dune." },
            new Author { FirstName = "Robert",  LastName = "Martin",     Biography = "Author of Clean Code." },
            new Author { FirstName = "Yuval",   LastName = "Harari",     Biography = "Author of Sapiens." },
            new Author { FirstName = "Fyodor",  LastName = "Dostoevsky", Biography = "Author of Crime and Punishment." }
        };
        db.Authors.AddRange(authors);
        db.SaveChanges();

        var books = new[]
        {
            new Book { Title = "1984",                  ISBN = "978-0451524935", PublishedYear = 1949, TotalCopies = 3, CategoryId = categories[0].Id, AuthorId = authors[0].Id },
            new Book { Title = "Animal Farm",           ISBN = "978-0451526342", PublishedYear = 1945, TotalCopies = 2, CategoryId = categories[0].Id, AuthorId = authors[0].Id },
            new Book { Title = "Dune",                  ISBN = "978-0441013593", PublishedYear = 1965, TotalCopies = 4, CategoryId = categories[0].Id, AuthorId = authors[1].Id },
            new Book { Title = "Clean Code",            ISBN = "978-0132350884", PublishedYear = 2008, TotalCopies = 5, CategoryId = categories[3].Id, AuthorId = authors[2].Id },
            new Book { Title = "Sapiens",               ISBN = "978-0062316097", PublishedYear = 2011, TotalCopies = 4, CategoryId = categories[1].Id, AuthorId = authors[3].Id },
            new Book { Title = "Crime and Punishment",  ISBN = "978-0486415871", PublishedYear = 1866, TotalCopies = 2, CategoryId = categories[0].Id, AuthorId = authors[4].Id }
        };
        db.Books.AddRange(books);
        db.SaveChanges();

        var readers = new[]
        {
            new Reader { FirstName = "Olena", LastName = "Kovalenko",  Email = "olena@example.com", Phone = "+380501234567", RegisteredAt = DateTime.UtcNow },
            new Reader { FirstName = "Ivan",  LastName = "Petrenko",   Email = "ivan@example.com",  Phone = "+380671234567", RegisteredAt = DateTime.UtcNow },
            new Reader { FirstName = "Maria", LastName = "Shevchenko", Email = "maria@example.com", Phone = "+380631234567", RegisteredAt = DateTime.UtcNow }
        };
        db.Readers.AddRange(readers);
        db.SaveChanges();

        db.Loans.AddRange(
            new Loan { BookId = books[0].Id, ReaderId = readers[0].Id, LoanDate = new DateTime(2025,1,10), DueDate = new DateTime(2025,1,24), ReturnDate = new DateTime(2025,1,22) },
            new Loan { BookId = books[3].Id, ReaderId = readers[1].Id, LoanDate = new DateTime(2025,2,1),  DueDate = new DateTime(2025,2,15), ReturnDate = null },
            new Loan { BookId = books[4].Id, ReaderId = readers[2].Id, LoanDate = new DateTime(2025,2,5),  DueDate = new DateTime(2025,2,19), ReturnDate = new DateTime(2025,2,18) }
        );
        db.SaveChanges();
    }
}