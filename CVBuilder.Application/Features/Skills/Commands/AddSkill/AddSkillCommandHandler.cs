using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, AddSkillCommandResponse>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public AddSkillCommandHandler(ISkillRepository skillRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        public async Task<AddSkillCommandResponse> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if(!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Skill),request.EmployeeId);

            #endregion

            
            #region mapping incoming request to skill entity

            var skillToAdd = mapper.Map<Skill>(request);

            #endregion

            var response = await skillRepository.AddAsync(skillToAdd);

            return mapper.Map<AddSkillCommandResponse>(response);
        }
    }
}
