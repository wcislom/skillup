using Shared.Abstractions.CQRS;

namespace BookStore.Application.Commands;

public record CreateBook(int Id) : ICommand
{
}
