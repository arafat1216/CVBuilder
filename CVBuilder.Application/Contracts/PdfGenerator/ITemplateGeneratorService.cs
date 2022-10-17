using CVBuilder.Application.Dtos.Employee;

namespace CVBuilder.Application.Contracts.PdfGenerator
{
    public interface ITemplateGeneratorService
    {
        Task<string> GenerateHtmlTemplate(EmployeeDetailsDto employeeDetails);
    }
}
