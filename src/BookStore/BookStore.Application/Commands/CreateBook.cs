using Shared.Abstractions.CQRS;

namespace BookStore.Application.Commands;

public record CreateBook(string Title, DateOnly PublishDate, decimal BasePrice, string AuthorFirstName, string AuthorLastName) : ICommand
{
}
