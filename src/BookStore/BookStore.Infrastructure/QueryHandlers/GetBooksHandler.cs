using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;

namespace BookStore.Infrastructure.QueryHandlers;

public class GetBooksHandler : IQueryHandler<GetBooks, IEnumerable<BookDto>>
{
    private static readonly string[] Titles = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<BookDto>> HandleAsync(GetBooks query)
    {
        var range = Enumerable.Range(1, 5).Select(index => new BookDto
        {
            Title = Titles[Random.Shared.Next(Titles.Length)]
        })
         .ToArray();

        return Task.FromResult(range.AsEnumerable());
    }

}
