using CVBuilder.Application.Dtos.Email;
using System.Threading.Tasks;

namespace FunctionEmailSenderApp.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto emailDto);
    }
}
