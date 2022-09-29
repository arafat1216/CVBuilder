using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience
{
    public class AddWorkExperinceCommandHandler : IRequestHandler<AddWorkExperienceCommand, AddWorkExperienceCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWorkExperienceRepository workExperienceRepository;
        private readonly IMapper mapper;

        public AddWorkExperinceCommandHandler(IEmployeeRepository employeeRepository, IWorkExperienceRepository workExperienceRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.workExperienceRepository = workExperienceRepository;
            this.mapper = mapper;
        }
        public async Task<AddWorkExperienceCommandResponse> Handle(AddWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            #region validate incoming request

            var validator = new AddWorkExperinceValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region mapping incoming request to work experience entity

            var workExperienceToCreate = mapper.Map<WorkExperience>(request);

            #endregion

            var response = await workExperienceRepository.AddAsync(workExperienceToCreate);

            return mapper.Map<AddWorkExperienceCommandResponse>(response);
        }
    }
}
