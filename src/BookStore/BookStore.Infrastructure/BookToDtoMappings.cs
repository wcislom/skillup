using BookStore.Application.Queries.DTO;

namespace BookStore.Infrastructure;

internal static class BookToDtoMappings
{
    public static BookDto AsDto(this object bookObj)
    {
        dynamic book = bookObj;

        return new BookDto(book.Id, book.Title, book.BasePrice, book.AuthorId, book.AuthorName);
    }
}
