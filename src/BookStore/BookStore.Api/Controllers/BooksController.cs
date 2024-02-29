using BookStore.Application.Commands;
using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.CQRS;
using System.Diagnostics;

namespace BookStore.Api.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;
    private readonly IQueryHandler<GetBooks, IEnumerable<BookDto>> _queryHandler;
    private readonly ICommandHandler<CreateBook> _createBook;
    public static readonly ActivitySource ActivitySource = new ActivitySource("SkillUp.BookStore.Controller");

    public BooksController(ILogger<BooksController> logger, IQueryHandler<GetBooks,
        IEnumerable<BookDto>> queryHandler,
        ICommandHandler<CreateBook> createBook)
    {
        _logger = logger;
        _queryHandler = queryHandler;
        _createBook = createBook;
    }

    [HttpGet(Name = "GetBooksWithAuthors")]
    [Authorize(Policy = "allow_read")]
    public async Task<IActionResult> Get()
    {
        using(_logger.BeginScope("BooksController.Get for user {userId}", User.Identity?.Name))
        using(var activity = ActivitySource.StartActivity("BrowseBooks"))
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


