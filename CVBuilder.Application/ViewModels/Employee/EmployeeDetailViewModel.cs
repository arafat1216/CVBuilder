using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.WorkExperience;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.ViewModels.Employee
{
    public class EmployeeDetailViewModel
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }


        public ICollection<SkillsListViewModel> Skills { get; set; }
        public ICollection<DegreesListViewModel> Degrees { get; set; }
        public ICollection<WorkExperiencesListViewModel> WorkExperiences { get; set; }
        public ICollection<ProjectsListViewModel> Projects { get; set; }

    }
}
