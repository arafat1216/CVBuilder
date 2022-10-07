using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Project
{
    public class ProjectViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
