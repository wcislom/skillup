using BookStore.Application.Commands;
using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.CQRS;
using System.Diagnostics;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IQueryHandler<GetBooks, IEnumerable<BookDto>> _queryHandler;
    private readonly ICommandHandler<CreateBook> _createBook;
    private readonly ActivitySource _activitySource;

    public BooksController(ILogger<BooksController> logger, IQueryHandler<GetBooks,
        IEnumerable<BookDto>> queryHandler,
        ICommandHandler<CreateBook> createBook)
    {
        _logger = logger;
        _queryHandler = queryHandler;
        _createBook = createBook;
        _activitySource = new ActivitySource("SkillUp.BookStore.Controller");

    }

    [HttpGet(Name = "GetBooksWithAuthors")]
    public async Task<IActionResult> Get()
    {
        using(_logger.BeginScope("BooksController.Get for user {userId}", User.Identity?.Name))
        using(var activity = _activitySource.StartActivity("BrowseBooks"))
        {
            _logger.LogInformation("Getting books with authors");
            return Ok(await _queryHandler.HandleAsync(new GetBooks()));
        }
    }

    [HttpPost(Name = "CreateBook")]
    public async Task<IActionResult> CreateBook([FromBody] CreateBook command, CancellationToken cancellation)
    {
        _logger.LogInformation("Creating a new book");
        await _createBook.Handle(command, cancellation);
        return Created("api", command);
    }
}


