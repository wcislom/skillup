using BookStore.Application.Queries.DTO;
using BookStore.Core;

namespace BookStore.Infrastructure.DAL.Mappings;

internal static class BookToDtoMappings
{
    public static BookDto AsDto(this Book book)
        => new BookDto
        {
            Title = book.Title,
            BasePrice = book.BasePrice,
            AuthorName = $"{book.Author.FirstName} {book.Author.LastName}"
        };
}
