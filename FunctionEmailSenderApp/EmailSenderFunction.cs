using CVBuilder.Application.Dtos.Email;
using FunctionEmailSenderApp.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionEmailSenderApp
{
    public class EmailSenderFunction
    {
        private readonly IEmailService emailService;

        public EmailSenderFunction(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [FunctionName("EmailSenderFunction")]
        public async Task Run([QueueTrigger("add-recipient", Connection = "AzureSettings")]string myQueueItem, ILogger log)
        {
            var emailDto = JsonSerializer.Deserialize<EmailDto>(myQueueItem);

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            await emailService.SendEmail(emailDto);
        }
    }
}
