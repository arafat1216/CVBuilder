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

        public async Task UploadEmailToQueue(EmailDto emailDto)
        {
            var queueClient = new QueueClient(AzureSettings.ConnectionString, AzureSettings.QueueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            var message = JsonSerializer.Serialize(emailDto);

            await queueClient.SendMessageAsync(message);
        }
    }
}
