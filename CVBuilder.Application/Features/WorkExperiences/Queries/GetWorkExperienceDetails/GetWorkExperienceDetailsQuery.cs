using CVBuilder.Application.Dtos.WorkExperience;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails
{
    public class GetWorkExperienceDetailsQuery : IRequest<WorkExperienceDetailsDto>
    {
        public Guid EmployeeId { get; set; }
        public int WorkExperienceId { get; set; }
    }
}
