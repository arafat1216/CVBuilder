namespace CVBuilder.Application.Features.Projects.Commands.AddProject
{
    public class AddProjectCommandResponse
    {
        public Guid EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
