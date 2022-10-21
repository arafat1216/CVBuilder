using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.UpdateResourceRequest
{
    public class AddDegreeRequestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Institute { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
