using CVBuilder.Application.Dtos.Skill;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillsList
{
    public class GetSkillsListQuery : IRequest<List<SkillDetailsDto>>
    {
        public Guid EmployeeId { get; set; }
    }
}
