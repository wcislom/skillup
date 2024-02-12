using BookStore.Core;
using BookStore.Core.Repositories;
using Shared.Abstractions.CQRS;

namespace BookStore.Application.Commands.Handlers;

internal class CreateBookHandler : ICommandHandler<CreateBook>
{
    public CreateBookHandler(IBookstoreRepository repository)
    {
    }

    public Task Handle(CreateBook command)
    {
        var book = new Book(command.Title, command.PublishDate, command.BasePrice);

        return Task.CompletedTask;
    }
}
