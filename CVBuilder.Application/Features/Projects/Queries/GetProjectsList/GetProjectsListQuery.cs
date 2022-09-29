using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQuery : IRequest<List<ProjectViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }
}
