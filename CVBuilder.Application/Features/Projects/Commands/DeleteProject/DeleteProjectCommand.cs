using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int ProjectId { get; set; }
    }
}
