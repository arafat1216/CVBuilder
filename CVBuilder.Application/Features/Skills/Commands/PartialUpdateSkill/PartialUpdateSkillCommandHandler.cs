using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill
{
    public class PartialUpdateSkillCommandHandler : IRequestHandler<PartialUpdateSkillCommand>
    {
        private readonly ISkillRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PartialUpdateSkillCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public PartialUpdateSkillCommandHandler(ISkillRepository repository, IMapper mapper, ILogger<PartialUpdateSkillCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }

        public async Task<Unit> Handle(PartialUpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await GetSkillDetails(request.EmployeeId, request.SkillId);

            if (skill == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);



            request = UpdateRequest(request, skill.SkillDetails);

            mapper.Map(request, skill);

            await repository.UpdateAsync(skill);

            logger.LogInformation($"Skill With Id: {request.SkillId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private PartialUpdateSkillCommand UpdateRequest(PartialUpdateSkillCommand request, SkillDetails skillDetails)
        {
            request.Name = request.Name ?? skillDetails.Name;

            return request;
        }

        private async Task<Skill?> GetSkillDetails(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
