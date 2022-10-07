using CVBuilder.Application.Dtos.WorkExperience;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList
{
    public class GetWorkExperiencesListQuery : IRequest<List<WorkExperienceDetailsDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
