namespace BookStore.Core;

public class Author(string firstName, string lastName)
{
    public int AuthorId { get; private set; }

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

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
