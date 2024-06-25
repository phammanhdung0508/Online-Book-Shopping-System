﻿using Application.Abstractions.Caching;
using Application.Abstractions.Services;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Infrastructure.Caching;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true).Build();

        services.AddDbContext<BookStoreDbContext>
            (options => options.UseSqlServer
            (config.GetConnectionString
            ("DefaultConnectionString")!));

        services.AddDistributedMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddSingleton<IJWTTokenProvider, JWTTokenProvider>();
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
