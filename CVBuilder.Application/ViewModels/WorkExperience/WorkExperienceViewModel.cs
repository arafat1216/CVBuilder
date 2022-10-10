using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.ViewModels.WorkExperience
{
    public class WorkExperienceViewModel
    {
        [Required]
        public string Designation { get; set; }
        
        [Required]
        public string Company { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
}
