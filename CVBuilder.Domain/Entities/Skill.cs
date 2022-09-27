namespace CVBuilder.Domain.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
