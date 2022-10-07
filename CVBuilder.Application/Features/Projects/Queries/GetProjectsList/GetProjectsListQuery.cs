using CVBuilder.Application.Dtos.Project;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQuery : IRequest<List<ProjectDetailsDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
