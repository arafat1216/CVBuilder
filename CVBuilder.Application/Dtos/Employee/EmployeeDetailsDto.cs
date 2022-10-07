using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Dtos.WorkExperience;

namespace CVBuilder.Application.Dtos.Employee
{
    public class EmployeeDetailsDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }


        public ICollection<SkillsListDto> Skills { get; set; }
        public ICollection<DegreesListDto> Degrees { get; set; }
        public ICollection<WorkExperiencesListDto> WorkExperiences { get; set; }
        public ICollection<ProjectsListDto> Projects { get; set; }

    }
}
