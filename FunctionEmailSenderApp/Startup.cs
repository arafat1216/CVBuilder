using CVBuilder.Application.Contracts.Email;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.Services.EmailService;

[assembly: FunctionsStartup(typeof(FunctionEmailSenderApp.Startup))]

namespace FunctionEmailSenderApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddScoped<IEmailService, EmailService>();
        
        }

        
    }
}
