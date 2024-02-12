namespace BookStore.Core;

public class Book
{
    // Author cannot be created by EF Core, so default empty constructor is needed
    protected Book()
    {
        
    }

    public Book(string title, DateOnly publishDate, decimal basePrice, int id = default)
    {
        Id = id;
        Title = title;
        PublishDate = publishDate;
        BasePrice = basePrice;
    }

    public int Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public DateOnly? PublishDate { get; private set; }

    public decimal BasePrice { get; private set; }

    public Author Author { get; private set; } = default!;

    public Cover? Cover { get; private set; }
}
