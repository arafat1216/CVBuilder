using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList
{
    public class GetWorkExperiencesListQuery : IRequest<List<WorkExperienceViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }
}
