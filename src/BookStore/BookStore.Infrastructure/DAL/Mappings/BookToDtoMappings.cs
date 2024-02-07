using BookStore.Application.Queries.DTO;

namespace BookStore.Infrastructure.DAL.Mappings;

internal static class BookToDtoMappings
{
    public static BookDto AsDto(this object bookObj)
    {
        dynamic book = bookObj;

        return new BookDto(book.Title, book.BasePrice, book.AuthorName);
    }
}
