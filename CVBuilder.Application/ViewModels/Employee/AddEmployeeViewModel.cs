using CVBuilder.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Employee
{
    public class AddEmployeeViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"(^(01){1}[3-9]{1}\d{8})$")]
        public string PhoneNo { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
