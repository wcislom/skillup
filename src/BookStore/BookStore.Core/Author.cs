namespace BookStore.Core;

public class Author(int id, string firstName, string lastName)
{
    public int Id { get; private set; } = id;

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
