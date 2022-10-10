using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Skill
{
    public class SkillViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
