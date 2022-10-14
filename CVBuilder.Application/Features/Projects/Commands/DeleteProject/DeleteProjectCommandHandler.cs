using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteProjectCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public DeleteProjectCommandHandler(IProjectRepository repository, IMapper mapper, ILogger<DeleteProjectCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            // fetch project

            var projectToDelete = await GetProjectToDelete(request.EmployeeId, request.ProjectId);


            if (projectToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);
            
            if (request.SoftDelete)
            {
                projectToDelete.IsDeleted = true;

                await repository.UpdateAsync(projectToDelete);

                logger.LogInformation($"Project With Id: {request.ProjectId} Soft Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

                return Unit.Value;
            }


            await repository.DeleteAsync(projectToDelete);

            logger.LogInformation($"Project With Id: {request.ProjectId} Hard Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Project?> GetProjectToDelete(Guid employeeId, int projectId)
        {
            return await repository.GetProjectByIdAsync(employeeId, projectId);
        }
    }
}
