using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommand : IRequest<AddSkillCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
