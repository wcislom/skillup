using BookStore.Application.Queries.DTO;
using BookStore.Core;

namespace BookStore.Infrastructure;

internal static class BookToDtoMappings
{
    public static BookDto AsDto(this Book book)
    {
        return new BookDto(book.Id, book.Title, book.BasePrice, book.Author.Id, book.Author.Name.FullName, book.Tags);
    }
}
