namespace CVBuilder.Application.Dtos.ResourceRequests
{
    public class ProjectUpdateRequestDto
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
    }
}
