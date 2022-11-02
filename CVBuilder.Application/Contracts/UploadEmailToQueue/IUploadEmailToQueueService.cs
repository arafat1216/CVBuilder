using CVBuilder.Application.Dtos.Email;

namespace CVBuilder.Application.Contracts.UploadEmailToQueue
{
    public interface IUploadEmailToQueueService
    {
        Task UploadEmailToQueue(EmailDto emailDto, byte[] attachment);
    }
}
