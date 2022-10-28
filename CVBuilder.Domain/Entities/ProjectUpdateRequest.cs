using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class ProjectUpdateRequest
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public ProjectDetails ProjectDetails { get; set; }

        // Navigation Property
        public int RequestId { get; set; }
        
    }
}
