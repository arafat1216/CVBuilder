using CVBuilder.Application.Dtos.Project;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQuery : IRequest<List<ProjectsListDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
