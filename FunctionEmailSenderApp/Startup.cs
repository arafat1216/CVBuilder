using CVBuilder.Application;
using CVBuilder.Infrastructure;
using FunctionEmailSenderApp.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(FunctionEmailSenderApp.Startup))]

namespace FunctionEmailSenderApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IEmailService, EmailService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);

            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}
