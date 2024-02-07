﻿using BookStore.Application.Core;
using BookStore.Application.Queries;
using BookStore.Application.Queries.DTO;
using BookStore.Infrastructure.DAL;
using BookStore.Infrastructure.DAL.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.QueryHandlers;

public class GetBooksHandler(BookstoreDbContext dbContext) : IQueryHandler<GetBooks, IEnumerable<BookDto>>
{
    private readonly BookstoreDbContext _dbContext = dbContext;

    public async Task<IEnumerable<BookDto>> HandleAsync(GetBooks query) => await _dbContext.Books.Select(b => b.AsDto()).ToListAsync();
}
