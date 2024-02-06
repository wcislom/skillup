namespace BookStore.Core;

public class Book(string title, DateOnly publishDate, decimal basePrice)
{
    public int BookId { get; private set; }

    public string Title { get; private set; } = title;

    public DateOnly PublishDate { get; private set; } = publishDate;

    public decimal BasePrice { get; private set; } = basePrice;

    public Author Author { get; private set; }

    public int AuthorId { get; private set; }

    public Cover Cover { get; private set; }
}
