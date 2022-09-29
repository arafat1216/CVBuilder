using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int WorkExperienceId { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
