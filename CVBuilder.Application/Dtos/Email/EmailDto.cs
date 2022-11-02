using CVBuilder.Application.ViewModels.SendEmail;

namespace CVBuilder.Application.Dtos.Email
{
    public class EmailDto
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public List<EmailAddress> Recipients { get; set; }
        public string AttachmentName { get; set; }

    }
}
