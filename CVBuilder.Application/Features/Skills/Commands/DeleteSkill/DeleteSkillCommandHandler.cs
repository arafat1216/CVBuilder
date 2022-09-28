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
            #region fetch skill  

            var skillToDelete = await repository.GetSkillByIdAsync(request.EmployeeId, request.SkillId);

            #endregion


            if (skillToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);



            await repository.DeleteAsync(skillToDelete);

            return Unit.Value;
        }
    }
}
