using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.WorkExperience;

namespace CVBuilder.Application.ViewModels.UpdateCV
{
    public class UpdateCVViewModel
    {
        
        public List<UpdateDegreeViewModel>? Degrees { get; set; }
        public List<UpdateProjectViewModel>? Projects { get; set; }
        public List<UpdateSkillViewModel>? Skills { get; set; }
        public List<UpdateWorkExperienceViewModel>? WorkExperiences { get; set; }
    }
}
