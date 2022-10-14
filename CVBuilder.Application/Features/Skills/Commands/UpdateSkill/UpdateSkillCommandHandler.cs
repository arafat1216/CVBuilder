using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateSkillCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper, ILogger<UpdateSkillCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.skillRepository = skillRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            // check if skill exists

            var skillExists = await SkillExists(request.EmployeeId, request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            
            // mapping incoming request to skill entity

            var skillToUpdate = mapper.Map<Skill>(request);

            
            await skillRepository.UpdateAsync(skillToUpdate);

            logger.LogInformation($"Skill With Id: {request.SkillId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<bool> SkillExists(Guid employeeId, int skillId)
        {
            return await skillRepository.ExistsAsync(employeeId, skillId);
        }
    }
}
