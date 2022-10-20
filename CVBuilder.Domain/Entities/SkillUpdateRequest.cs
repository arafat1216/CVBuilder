namespace CVBuilder.Domain.Entities
{
    public class SkillUpdateRequest
    {
        public int Id { get; set; }
        public int? SkillId { get; set; }
        public string? Name { get; set; }

        // Navigation Property
        public int RequestId { get; set; }
    }
}
