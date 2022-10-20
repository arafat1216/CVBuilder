namespace CVBuilder.Domain.Entities
{
    public class ProjectUpdateRequest
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }

        // Navigation Property

        public int RequestId { get; set; }
        
    }
}
