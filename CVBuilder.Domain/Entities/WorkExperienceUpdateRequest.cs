using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class WorkExperienceUpdateRequest 
    {
        public int Id { get; set; }
        public int? WorkExperienceId { get; set; }
        public WorkExperienceDetails WorkExperienceDetails { get; set; }

        // Navigation Property
        public int RequestId { get; set; }
    }
}
