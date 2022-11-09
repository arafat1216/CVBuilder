using CVBuilder.Application.Contracts.Email;
using CVBuilder.Application.Dtos.Email;
using CVBuilder.Application.ViewModels.SendEmail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Net.Mime;

namespace Shared.Common.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(EmailDto emailDto, byte[] file)
        {
            var recipientsList = GetRecipientsList(emailDto.Recipients);

            MimeMessage email = GenerateEmail(emailDto, recipientsList, file);

            using var smpt = new SmtpClient();

            smpt.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);

            smpt.Authenticate("trace.mante63@ethereal.email", "B51eXRye1szq4ubrer");

            await smpt.SendAsync(email);

            smpt.Disconnect(true);
        }

        private MimeMessage GenerateEmail(EmailDto emailDto, InternetAddressList recipientsList, byte[] file)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(emailDto.Sender));

            email.To.AddRange(recipientsList);

            email.Subject = "My CV";

            var builder = new BodyBuilder();

            builder.HtmlBody = "<h1>Hi, Please Checkout My CV Attached with This Email</h1>";

            builder.TextBody = "Thank You";

            builder.Attachments.Add("resume.pdf", file, MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf));

            email.Body = builder.ToMessageBody();

            return email;
        }

        private InternetAddressList GetRecipientsList(List<EmailAddress> recipients)
        {
            var recipientsList = new InternetAddressList();

            foreach (var item in recipients)
            {
                recipientsList.Add(MailboxAddress.Parse(item.Email));
            }

            return recipientsList;
        }
    }
}
