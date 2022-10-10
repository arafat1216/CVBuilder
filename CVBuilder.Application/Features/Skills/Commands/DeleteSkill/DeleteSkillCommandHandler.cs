using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
    {
        private readonly ISkillRepository repository;

        public DeleteSkillCommandHandler(ISkillRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            // fetch skill  

            var skillToDelete = await GetSkillToDelete(request.EmployeeId, request.SkillId);

            if (skillToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);


            await repository.DeleteAsync(skillToDelete);

            return Unit.Value;
        }

        private async Task<Skill?> GetSkillToDelete(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
