using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Company
{
    public class CompanyLoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
