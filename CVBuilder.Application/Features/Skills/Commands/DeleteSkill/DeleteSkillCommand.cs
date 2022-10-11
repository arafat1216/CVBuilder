using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public int SkillId { get; set; }
        public bool SoftDelete { get; set; }
    }
}
