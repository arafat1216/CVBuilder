using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.AddProject
{
    public class AddProjectCommand : IRequest<AddProjectCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
