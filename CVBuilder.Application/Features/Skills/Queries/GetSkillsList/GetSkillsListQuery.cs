using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillsList
{
    public class GetSkillsListQuery : IRequest<List<SkillViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }
}
