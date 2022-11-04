using CVBuilder.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Company
{
    public class RegisterViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"(^(01){1}[3-9]{1}\d{8})$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public SubscriptionType SubscriptionType { get; set; }
    }
}
