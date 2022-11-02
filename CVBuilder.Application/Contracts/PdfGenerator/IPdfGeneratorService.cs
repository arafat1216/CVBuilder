using CVBuilder.Application.Dtos.Employee;

namespace CVBuilder.Application.Contracts.PdfGenerator
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GeneratePdf(Guid employeeId);
    }
}
