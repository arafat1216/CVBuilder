using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsQuery : IRequest<ProjectViewModel>
    {
        public Guid EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
