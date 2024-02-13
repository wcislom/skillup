﻿namespace BookStore.Application.Queries.DTO;

public record BookDto(int Id, string Title, decimal BasePrice, int AuthorId, string AuthorName)
{
}
