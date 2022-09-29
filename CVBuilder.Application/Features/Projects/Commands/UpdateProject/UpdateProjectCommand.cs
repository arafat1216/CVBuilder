using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
