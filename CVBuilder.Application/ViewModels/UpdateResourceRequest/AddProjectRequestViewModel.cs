using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.UpdateResourceRequest
{
    public class AddProjectRequestViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Link { get; set; }
        
        [Required]
        public string Reason { get; set; }

    }
}
