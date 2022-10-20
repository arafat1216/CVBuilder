using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.UpdateResourceRequest
{
    public class AddSkillRequestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
