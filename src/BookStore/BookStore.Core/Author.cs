namespace BookStore.Core;

public class Author
{
    protected Author()
    { 
    }

    public Author(string firstName, string lastName, int id = default)
    {
        Name = new PersonName(firstName, lastName); 
        Id = id;
    }

    public int Id { get; private set; }

    public PersonName Name { get; private set; } = default!;

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
