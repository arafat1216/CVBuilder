using CVBuilder.Application.Dtos.Project;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsQuery : IRequest<ProjectDetailsDto>
    {
        public Guid EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
