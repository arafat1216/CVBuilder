using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string Name { get; set; }
    }
}
