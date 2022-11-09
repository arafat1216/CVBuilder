using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Dtos.Employee;
using Razor.Templating.Core;

namespace Shared.Common.Services.TemplateGenerator
{
    public class TemplateGeneratorService : ITemplateGeneratorService
    {
        public async Task<string> GenerateHtmlTemplate(EmployeeDetailsDto employeeDetails)
        {
            var html = await RazorTemplateEngine.RenderAsync("~/Templates/PdfTemplate.cshtml", employeeDetails);

            return html;
        }
    }
}
