using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using CVBuilder.Application.Contracts.UploadEmailToQueue;
using CVBuilder.Application.Dtos.Email;
using CVBuilder.Application.Models.Azure;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CVBuilder.Infrastructure.Services
{
    public class UploadEmailToQueueService : IUploadEmailToQueueService
    {
        public UploadEmailToQueueService(IOptions<AzureSettings> azureSettings) 
        {
            AzureSettings = azureSettings.Value;
        }

        public AzureSettings AzureSettings { get; set; }

        public async Task UploadEmailToQueue(EmailDto emailDto, byte[] attachment)
        {
            var queueClient = new QueueClient(AzureSettings.ConnectionString, AzureSettings.QueueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            emailDto.AttachmentName = await UploadAttachment(attachment);

            var message = JsonSerializer.Serialize(emailDto);

            await queueClient.SendMessageAsync(message);
        }

        private async Task<string> UploadAttachment(byte[] attachment)
        {
            var container = new BlobContainerClient(AzureSettings.ConnectionString, AzureSettings.ContainerName);

            Guid guid = Guid.NewGuid();

            string fileName = guid.ToString() +".pdf";

            using (var stream = new MemoryStream(attachment))
            {
                await container.UploadBlobAsync(fileName, stream);
            }

            return fileName;
        }
    }
}
