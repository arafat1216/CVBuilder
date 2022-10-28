namespace CVBuilder.Application.Dtos.Project
{
    public class ProjectsListDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
