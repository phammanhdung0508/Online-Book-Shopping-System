using Application.Books.Commands.CreateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.ImportFromExcel;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetAllBook;
using Application.Books.Queries.GetBooks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[ApiVersion(1)]
[ApiVersion(2, Deprecated = true)]
[Route("api/v{v:apiVersion}/books")]
public sealed class BookController : ApiController
{
    public BookController(ISender sender)
        : base(sender)
    { }

    [MapToApiVersion(1)]
    [HttpGet("")]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken,
        [FromQuery] int pageSize,
        [FromQuery] int pageIndex,
        [FromQuery] string? filter = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? sortBy = null)
    {
        var query = new GetBooksQuery
            (pageIndex, pageSize, filter, sort, sortBy, null);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpGet("detail")]
    public async Task<IActionResult> GetById(
        GetBookByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPost("import")]
    public async Task<IActionResult> ImportFromExcel(
        [FromBody] ImportFromExcelCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(
        [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [MapToApiVersion(1)]
    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
