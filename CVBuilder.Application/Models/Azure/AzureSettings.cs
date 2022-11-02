namespace CVBuilder.Application.Models.Azure
{
    public class AzureSettings
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
        public string ContainerName { get; set; }
    }
}
