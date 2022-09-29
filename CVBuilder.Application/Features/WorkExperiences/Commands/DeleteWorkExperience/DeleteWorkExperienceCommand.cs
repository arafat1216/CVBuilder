using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience
{
    public class DeleteWorkExperienceCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int WorkExperienceId { get; set; }
    }
}
