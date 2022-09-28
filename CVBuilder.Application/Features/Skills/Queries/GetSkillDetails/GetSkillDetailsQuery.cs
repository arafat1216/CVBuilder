using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillDetails
{
    public class GetSkillDetailsQuery : IRequest<SkillViewModel>
    {
        public Guid EmployeeId { get; set; }
        public int SkillId { get; set; }
    }
}
