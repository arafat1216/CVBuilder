using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.Degree
{
    public class DegreeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Institute { get; set; }
    }
}
