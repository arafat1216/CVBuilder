using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience
{
    public class AddWorkExperienceCommand : IRequest<AddWorkExperienceCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
