using Application.Books.Queries.GetBook;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Queries.GetAllBook;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.ImportFromExcel;

namespace Presentation.Controllers;

[Route("api/books")]
public sealed class BookController : ApiController
{
    public BookController(ISender sender)
        : base(sender)
    { }

    [HttpGet("")]
    public async Task<IActionResult> Get(
        [FromQuery] string filter,
        [FromQuery] string sort,
        [FromQuery] int pageSize,
        [FromQuery] int pageIndex,
        CancellationToken cancellationToken)
    {
        Sort s;
        switch (sort)
        {
            case "asc":
                s = Sort.Ascending;
                break;
            case "desc":
                s = Sort.Descending;
                break;
            default:
                s = Sort.NoSort;
                break;
        }
        
        var query = new GetBookQuery(filter, s, "");

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpGet("detail")]
    public async Task<IActionResult> GetById(
        GetBookByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportFromExcel(
        [FromBody] ImportFromExcelCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpPut("edit")]
    public async Task<IActionResult> Edit(
        [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteBookCommand query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
    }
}
