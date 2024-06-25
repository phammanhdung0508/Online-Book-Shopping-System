using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Abstractions.IRepository;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using OfficeOpenXml;

namespace Application.Books.Commands.ImportFromExcel;

internal sealed class ImportFromExcelCommandHandler
    : ICommandHandler<ImportFromExcelCommand>
{
    private readonly IBookRepository bookRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public ImportFromExcelCommandHandler(
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.bookRepository = bookRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task<Result> Handle(ImportFromExcelCommand request, CancellationToken cancellationToken)
    {
        using (var stream = new MemoryStream())
        {
            await request.file.CopyToAsync(stream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowcount = worksheet.Dimension.Rows;

                if(worksheet is null)
                {
                    return Result.Failure(Error.NullValue);
                }

                for (int row = 2; row <= rowcount; row++)
                {
                    var book = new Book.Builder()
                        .SetID(Guid.NewGuid())
                        .SetTitle(worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString()!)
                        .SetAuthor(worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString()!)
                        .SetPublishedYear(int.Parse(worksheet.Cells[row, 9].Value.ToString()!))
                        .SetPublisher(worksheet.Cells[row, 8].Value == null ? "" : worksheet.Cells[row, 8].Value.ToString()!)
                        .SetISBN(worksheet.Cells[row, 2].Value == null ? "0" : worksheet.Cells[row, 2].Value.ToString()!)
                        .SetPrice(double.Parse(worksheet.Cells[row, 2].Value == null ? "0" : worksheet.Cells[row, 2].Value.ToString()!))
                        .SetUnitInStock(int.Parse(worksheet.Cells[row, 3].Value == null ? "0" : worksheet.Cells[row, 3].Value.ToString()!))
                        .Build();

                    bookRepository.Create(book);
                };

                await unitOfWork.SaveChangeAsync(cancellationToken);
            }
        }

        return Result.Success();
    }
}