using Shared.Abstractions.CQRS;

namespace BookStore.Application.Commands.Handlers;

internal class CreateBookHandler : ICommandHandler<CreateBook>
{
    public Task Handle(CreateBook command)
    {
        return Task.CompletedTask;
    }
}
