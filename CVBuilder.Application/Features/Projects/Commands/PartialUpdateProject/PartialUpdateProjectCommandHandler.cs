using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject
{
    public class PartialUpdateProjectCommandHandler : IRequestHandler<PartialUpdateProjectCommand>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public PartialUpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(PartialUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDetails = await GetProjectDetails(request.EmployeeId, request.ProjectId);

            if (projectDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.ProjectId);


            mapper.Map(request, projectDetails);

            await repository.UpdateAsync(projectDetails);

            return Unit.Value;
        }

        private async Task<Project?> GetProjectDetails(Guid employeeId, int projectId)
        {
            return await repository.GetProjectByIdAsync(employeeId, projectId);
        }
    }
}
