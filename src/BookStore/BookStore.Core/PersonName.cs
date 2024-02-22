namespace BookStore.Core;

public record PersonName(string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}
