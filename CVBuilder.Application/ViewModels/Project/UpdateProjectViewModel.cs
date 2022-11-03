using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Project
{
    public class UpdateProjectViewModel
    {
        public int? ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string Reason { get; set; }
    }
}
