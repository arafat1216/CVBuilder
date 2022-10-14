using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, AddSkillCommandResponse>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AddSkillCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public AddSkillCommandHandler(ISkillRepository skillRepository, IEmployeeRepository employeeRepository, IMapper mapper, ILogger<AddSkillCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.skillRepository = skillRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<AddSkillCommandResponse> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            // check if employee exists

            var employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.EmployeeId);


            // mapping incoming request to skill entity

            var skillToAdd = mapper.Map<Skill>(request);

            var response = await skillRepository.AddAsync(skillToAdd);

            logger.LogInformation($"Skill With Id: {response.SkillId} Added For Employee: {response.EmployeeId} By {applicationUser.GetUserId()}");


            return mapper.Map<AddSkillCommandResponse>(response);
        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
