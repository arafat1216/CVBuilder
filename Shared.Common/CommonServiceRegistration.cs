using CVBuilder.Application.Contracts.PdfGenerator;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.Services.PdfGenerator;
using Shared.Common.Services.TemplateGenerator;

namespace Shared.Common
{
    public static class CommonServiceRegistration
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ITemplateGeneratorService, TemplateGeneratorService>();

            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return services;
        }
    }
}
