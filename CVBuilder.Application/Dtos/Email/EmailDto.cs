using CVBuilder.Application.ViewModels.SendEmail;

namespace CVBuilder.Application.Dtos.Email
{
    public class EmailDto
    {
        public Guid SenderId { get; set; }
        public List<EmailAddress> Recipients { get; set; }
    }
}
