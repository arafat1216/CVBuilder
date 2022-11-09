using CVBuilder.Application.Dtos.Email;

namespace CVBuilder.Application.Contracts.Email
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto emailDto, byte[] file);
    }
}
