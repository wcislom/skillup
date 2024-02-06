namespace BookStore.Core;

public class Artist (string firstName, string lastName)
{
    public int ArtistId { get; private set; }

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public List<Cover> Covers { get; private set; } = new();
}
