using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Books.Commands.ImportFromExcel;

public sealed record ImportFromExcelCommand(
    IFormFile file) : ICommand;