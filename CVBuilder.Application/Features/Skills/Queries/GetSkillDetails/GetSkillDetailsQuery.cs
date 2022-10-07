using CVBuilder.Application.Dtos.Skill;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillDetails
{
    public class GetSkillDetailsQuery : IRequest<SkillDetailsDto>
    {
        public Guid EmployeeId { get; set; }
        public int SkillId { get; set; }
    }
}
