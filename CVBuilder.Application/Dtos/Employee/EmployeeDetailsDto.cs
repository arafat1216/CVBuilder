using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Dtos.WorkExperience;

namespace CVBuilder.Application.Dtos.Employee
{
    public class EmployeeDetailsDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }


        public IList<SkillsListDto> Skills { get; set; }
        public IList<DegreesListDto> Degrees { get; set; }
        public IList<WorkExperiencesListDto> WorkExperiences { get; set; }
        public IList<ProjectsListDto> Projects { get; set; }

    }
}
