using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject
{
    public class PartialUpdateProjectCommandHandler : IRequestHandler<PartialUpdateProjectCommand>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PartialUpdateProjectCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public PartialUpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper, ILogger<PartialUpdateProjectCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(PartialUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDetails = await GetProjectDetails(request.EmployeeId, request.ProjectId);

            if (projectDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.ProjectId);


            mapper.Map(request, projectDetails);

            await repository.UpdateAsync(projectDetails);

            logger.LogInformation($"Project With Id: {request.ProjectId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Project?> GetProjectDetails(Guid employeeId, int projectId)
        {
            return await repository.GetProjectByIdAsync(employeeId, projectId);
        }
    }
}
