using Azure.Storage.Blobs;
using CVBuilder.Application.Dtos.Email;
using CVBuilder.Application.ViewModels.SendEmail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc.Formatters;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FunctionEmailSenderApp.Services
{
    public class EmailService : IEmailService
    {
        private string connectionString = "DefaultEndpointsProtocol=https;AccountName=cvbuilderaccount;AccountKey=6Kdd/uBEHAlWNdNbTnDc3Sgzq6xnPZUG2h2IO5BYxwQRxPrPEbac335utQwY8/thVclsNetDhmBc+AStM/aBdA==;EndpointSuffix=core.windows.net";

        private string containerName = "attachments";

        public async Task SendEmail(EmailDto emailDto)
        {
            var recipientsList = GetRecipientsList(emailDto.Recipients);

            byte[] file =  await GetFile(emailDto.AttachmentName);

            await DeleteFile(emailDto.AttachmentName);

            MimeMessage email = GenerateEmail(emailDto, recipientsList, file);

            using var smpt = new SmtpClient();

            smpt.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);

            smpt.Authenticate("kyra17@ethereal.email", "khCqgaDfbxXbQvtY3M");

            await smpt.SendAsync(email);

            smpt.Disconnect(true);
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

        private static MimeMessage GenerateEmail(EmailDto emailDto, InternetAddressList recipientsList, byte[] file)
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
