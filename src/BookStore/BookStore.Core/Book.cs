namespace BookStore.Core;

public class Book
{
    // Author cannot be created by EF Core, so default empty constructor is needed
    protected Book()
    {
        
    }

    public Book(int id, string title, DateOnly publishDate, decimal basePrice, int authorId)
    {
        Id = id;
        Title = title;
        PublishDate = publishDate;
        BasePrice = basePrice;
        AuthorId = authorId;
    }

    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public DateOnly? PublishDate { get; private set; }

    public decimal BasePrice { get; private set; }

    public Author? Author { get; private set; }

    public int AuthorId { get; private set; }

    public Cover? Cover { get; private set; }
}
