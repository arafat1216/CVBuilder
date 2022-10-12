using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill
{
    public class PartialUpdateSkillCommandHandler : IRequestHandler<PartialUpdateSkillCommand>
    {
        private readonly ISkillRepository repository;

        public PartialUpdateSkillCommandHandler(ISkillRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(PartialUpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skillDetails = await GetSkillDetails(request.EmployeeId, request.SkillId);

            if (skillDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            skillDetails.Name = request.Name ?? skillDetails.Name;

            await repository.UpdateAsync(skillDetails);

            return Unit.Value;
        }

        private async Task<Skill?> GetSkillDetails(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
