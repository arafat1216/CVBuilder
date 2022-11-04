using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Company
{
     public class UpdateCompanyDetailsViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"(^(01){1}[3-9]{1}\d{8})$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNo { get; set; }
    }
}
