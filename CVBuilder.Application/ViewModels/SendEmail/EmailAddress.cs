using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.SendEmail
{
    public class EmailAddress
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
