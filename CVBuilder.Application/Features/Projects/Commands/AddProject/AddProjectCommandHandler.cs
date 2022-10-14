using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Projects.Commands.AddProject
{
    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, AddProjectCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AddProjectCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public AddProjectCommandHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper, ILogger<AddProjectCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<AddProjectCommandResponse> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            // check if employee exists

            var employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);



            // mapping incoming request to project entity

            var projectToCreate = mapper.Map<Project>(request);

            
            var response = await projectRepository.AddAsync(projectToCreate);

            logger.LogInformation($"Project With Id: {response.ProjectId} Added For Employee: {response.EmployeeId} By {applicationUser.GetUserId()}");

            return mapper.Map<AddProjectCommandResponse>(response);
        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
