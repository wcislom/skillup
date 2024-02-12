namespace BookStore.Core;

public class Author
{
    public Author(string firstName, string lastName, int id = default)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }

    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public List<Book> Books { get; private set; } = new();

    public void AddBook(Book book)
    {
        if(book is null)
        {
            throw new ArgumentNullException(nameof(book), "Book cannot be null");
        }

        Books.Add(book);
    }
}
