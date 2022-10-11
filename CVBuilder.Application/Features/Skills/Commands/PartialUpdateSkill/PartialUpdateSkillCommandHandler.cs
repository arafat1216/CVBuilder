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
            var skillDetails = await repository.GetSkillByIdAsync(request.EmployeeId, request.SkillId);

            if (skillDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            skillDetails.Name = request.Name ?? skillDetails.Name;

            await repository.UpdateAsync(skillDetails);

            return Unit.Value;
        }
    }
}
