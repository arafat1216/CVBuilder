using CVBuilder.Domain.ValueObjects;

namespace CVBuilder.Domain.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public SkillDetails SkillDetails { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public Guid EmployeeId { get; set; }

    }
}
