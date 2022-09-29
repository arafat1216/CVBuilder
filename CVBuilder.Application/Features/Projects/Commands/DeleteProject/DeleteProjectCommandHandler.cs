using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public DeleteProjectCommandHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            #region fetch project

            var projectToDelete = await repository.GetProjectByIdAsync(request.EmployeeId, request.ProjectId);

            #endregion

            if (projectToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            await repository.DeleteAsync(projectToDelete);

            return Unit.Value;  
        }
    }
}
