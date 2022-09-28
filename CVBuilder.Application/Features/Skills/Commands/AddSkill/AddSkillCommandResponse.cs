namespace CVBuilder.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommandResponse
    {
        public int SkillId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
