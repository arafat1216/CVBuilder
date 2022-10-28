using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class WorkExperience
    {
        public int WorkExperienceId { get; set; }

        public WorkExperienceDetails WorkExperienceDetails { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public Guid EmployeeId { get; set; }
    }
}
