namespace BookStore.Application.Exceptions;

[Serializable]
internal class AuthorDoesNotExistException : Exception
{
    public int AuthorId;

    public AuthorDoesNotExistException(int authorId) : base("Author {authorId} does not exists")
    {
        AuthorId = authorId;
    }
}