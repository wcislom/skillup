using Shared.Core;

namespace BookStore.Core.Exceptions;

[Serializable]
public class AuthorDoesNotExistException : DomainException
{
    public int AuthorId { get; set; }

    public AuthorDoesNotExistException(int authorId) : base($"Author with id '{authorId}' does not exist")
    {
        AuthorId = authorId;
    }
}