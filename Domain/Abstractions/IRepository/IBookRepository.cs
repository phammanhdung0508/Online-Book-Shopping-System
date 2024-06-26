﻿using Domain.Entities;
using Domain.Enum;

namespace Domain.Abstractions.IRepository;

public interface IBookRepository
{
    Task<List<Book>> Get(
        int pageIndex,
        int pageSize,
        string? filter,
        string? sort,
        string? sortBy,
        string? includeProperties,
        CancellationToken cancellationToken = default);
    Task<Book?> GetById(Guid Id, CancellationToken cancellationToken = default);
    void Create(Book book);
    void Update(Book book);
    void Delete(Guid Id);
}