using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectViewModel>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public GetProjectDetailsQueryHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProjectViewModel> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            #region check if project exists

            var projectExists = await repository.ExistsAsync(request.EmployeeId, request.ProjectId);

            if (!projectExists)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            #endregion

            #region fetch project details

            var projectDetails = await repository.GetProjectByIdAsync(request.EmployeeId, request.ProjectId);

            #endregion

            return mapper.Map<ProjectViewModel>(projectDetails);
        }
    }
}
