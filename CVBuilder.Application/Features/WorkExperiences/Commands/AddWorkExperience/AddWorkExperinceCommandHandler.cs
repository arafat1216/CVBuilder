using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience
{
    public class AddWorkExperinceCommandHandler : IRequestHandler<AddWorkExperienceCommand, AddWorkExperienceCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWorkExperienceRepository workExperienceRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AddWorkExperinceCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public AddWorkExperinceCommandHandler(IEmployeeRepository employeeRepository, IWorkExperienceRepository workExperienceRepository, IMapper mapper, ILogger<AddWorkExperinceCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.employeeRepository = employeeRepository;
            this.workExperienceRepository = workExperienceRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<AddWorkExperienceCommandResponse> Handle(AddWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            // check if employee exists

            var employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);



            // mapping incoming request to work experience entity

            var workExperienceToCreate = mapper.Map<WorkExperience>(request);

            
            var response = await workExperienceRepository.AddAsync(workExperienceToCreate);

            logger.LogInformation($"Work Experience With Id: {response.WorkExperienceId} Added For Employee: {response.EmployeeId} By {applicationUser.GetUserId()}");

            return mapper.Map<AddWorkExperienceCommandResponse>(response);
        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
