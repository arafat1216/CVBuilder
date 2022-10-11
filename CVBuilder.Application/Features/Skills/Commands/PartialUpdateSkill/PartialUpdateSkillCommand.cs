using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill
{
    public class PartialUpdateSkillCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string? Name { get; set; }
    }
}
