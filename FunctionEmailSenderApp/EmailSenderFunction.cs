using Azure.Storage.Blobs;
using CVBuilder.Application.Contracts.Email;
using CVBuilder.Application.Dtos.Email;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionEmailSenderApp
{
    public class EmailSenderFunction
    {
        private readonly IEmailService emailService;
        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=cvbuilderaccount;AccountKey=6Kdd/uBEHAlWNdNbTnDc3Sgzq6xnPZUG2h2IO5BYxwQRxPrPEbac335utQwY8/thVclsNetDhmBc+AStM/aBdA==;EndpointSuffix=core.windows.net";

        private string containerName = "attachments";

        public EmailSenderFunction(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [FunctionName("EmailSenderFunction")]
        public async Task Run([QueueTrigger("add-recipient", Connection = "AzureSettings")]string myQueueItem, ILogger log)
        {
            var emailDto = JsonSerializer.Deserialize<EmailDto>(myQueueItem);

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            byte[] file = await GetFile(emailDto.AttachmentName);

            await emailService.SendEmail(emailDto, file);

            await DeleteFile(emailDto.AttachmentName);
        }

        private async Task DeleteFile(string attachmentName)
        {
            var container = new BlobContainerClient(connectionString, containerName);

            var blob = container.GetBlobClient(attachmentName);

            if (await blob.ExistsAsync())
            {
                await blob.DeleteIfExistsAsync();
            }
        }

        private async Task<byte[]> GetFile(string attachmentName)
        {
            var container = new BlobContainerClient(connectionString, containerName);

            var blob = container.GetBlobClient(attachmentName);

            using (var ms = new MemoryStream())
            {
                await blob.DownloadToAsync(ms);

                return ms.ToArray();
            }
        }
    }
}
