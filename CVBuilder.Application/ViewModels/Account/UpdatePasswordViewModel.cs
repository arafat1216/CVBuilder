using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Account
{
    public class UpdatePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
