using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using DinkToPdf;
using DinkToPdf.Contracts;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        private readonly ITemplateGeneratorService templateGeneratorService;
        private readonly IConverter converter;
        private readonly IMediator mediator;

        public PdfGeneratorService(ITemplateGeneratorService templateGeneratorService, IConverter converter, IMediator mediator)
        {
            this.templateGeneratorService = templateGeneratorService;
            this.converter = converter;
            this.mediator = mediator;
        }
        public async Task<byte[]> GeneratePdf(Guid employeeId)
        {
            EmployeeDetailsDto employeeDetails = await GetEmployeeDetails(employeeId);

            var html = await templateGeneratorService.GenerateHtmlTemplate(employeeDetails);

            var globalSettings = GetGlobalSettings();

            var objectSettings = GetObjectSettings(html);

            var pdf = GetPdfDocument(globalSettings, objectSettings);

            return converter.Convert(pdf);
        }

        private async Task<EmployeeDetailsDto> GetEmployeeDetails(Guid employeeId)
        {
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = employeeId
            };

            var employeeDetails = await mediator.Send(requestDto);
            return employeeDetails;
        }

        private HtmlToPdfDocument GetPdfDocument(GlobalSettings globalSettings, ObjectSettings objectSettings)
        {
            return new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };
        }

        private ObjectSettings GetObjectSettings(string html)
        {
            return new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = new WebSettings() { DefaultEncoding = "utf-8"},
                HeaderSettings = GetHeaderSettings(),
                FooterSettings = GetFooterSettings(),
            };

        }

        private FooterSettings GetFooterSettings()
        {
            return new FooterSettings()
            {
                FontName = "Arial",
                FontSize = 9
            };
        }

        private HeaderSettings GetHeaderSettings()
        {
            return new HeaderSettings()
            {
                FontName = "Arial",
                FontSize = 9
            };
        }

        private GlobalSettings GetGlobalSettings()
        {
            return new GlobalSettings()
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10, Bottom = 10}
            };
        }
    }
}
