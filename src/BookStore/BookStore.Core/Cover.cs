namespace BookStore.Core;

public class Cover(string designIdeas, bool digitalOnly)
{
    public int Id { get; private set; }

    public string DesignIdeas { get; private set; } = designIdeas;

    public bool DigitalOnly { get; private set; } = digitalOnly;

    public List<Artist> Artists { get; private set; } = new();

    public Book? Book { get; private set; }

    public int BookId { get; private set; }
}