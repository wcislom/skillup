using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IQueryHandler<GetBooks, IEnumerable<BookDto>> _queryHandler;

    public BooksController(ILogger<BooksController> logger, IQueryHandler<GetBooks, IEnumerable<BookDto>> queryHandler)
    {
        _logger = logger;
        _queryHandler = queryHandler;
    }

    [HttpGet(Name = "GetBooksWithAuthors")]
    public async Task<IEnumerable<BookDto>> Get()
    {
        _logger.LogInformation("Getting books with authors");
        return await _queryHandler.HandleAsync(new GetBooks());
    }
}
