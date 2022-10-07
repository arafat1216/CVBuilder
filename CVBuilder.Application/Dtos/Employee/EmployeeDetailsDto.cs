using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.WorkExperience;

namespace CVBuilder.Application.Dtos.Employee
{
    public class EmployeeDetailsDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }


        public ICollection<SkillsListViewModel> Skills { get; set; }
        public ICollection<DegreesListDto> Degrees { get; set; }
        public ICollection<WorkExperiencesListViewModel> WorkExperiences { get; set; }
        public ICollection<ProjectsListDto> Projects { get; set; }

    }
}
