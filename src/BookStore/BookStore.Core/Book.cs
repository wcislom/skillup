namespace BookStore.Core;

public class Book
{
    protected Book()
    {
        
    }

    // Author cannot be created by EF Core, so default empty constructor is needed
    public Book(string title, DateOnly publishDate, decimal basePrice, Author author)
    {
        Title = title;
        PublishDate = publishDate;
        BasePrice = basePrice;
        Author = author;
    }

    public int BookId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public DateOnly? PublishDate { get; private set; }

    public decimal BasePrice { get; private set; }

    public Author Author { get; private set; }

    public int AuthorId { get; private set; }

    public Cover? Cover { get; private set; }
}
