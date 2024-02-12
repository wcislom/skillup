using BookStore.Application.Exceptions;
using BookStore.Core;
using BookStore.Core.Repositories;
using Shared.Abstractions.CQRS;

namespace BookStore.Application.Commands.Handlers;

internal class CreateBookHandler : ICommandHandler<CreateBook>
{
    private readonly IBookstoreRepository _repository;

    public CreateBookHandler(IBookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateBook command, CancellationToken cancellationToken)
    {
        var author = await _repository.GetAuthor(command.AuthorId, cancellationToken);
        if (author == null)
        {
            throw new AuthorDoesNotExistException(command.AuthorId);
        }

        var book = new Book(command.Title, command.PublishDate, command.BasePrice);
        author.AddBook(book);

        await _repository.SaveChanges(cancellationToken);
    }
}
