using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
    {
        private readonly ISkillRepository repository;
        private readonly ILogger<DeleteSkillCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public DeleteSkillCommandHandler(ISkillRepository repository, ILogger<DeleteSkillCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            // fetch skill  

            var skillToDelete = await GetSkillToDelete(request.EmployeeId, request.SkillId);

            if (skillToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);


            if (request.SoftDelete)
            {
                skillToDelete.IsDeleted = true;

                await repository.UpdateAsync(skillToDelete);

                logger.LogInformation($"Skill With Id: {request.SkillId} Soft Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

                return Unit.Value;
            }


            await repository.DeleteAsync(skillToDelete);

            logger.LogInformation($"Skill With Id: {request.SkillId} Hard Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Skill?> GetSkillToDelete(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
