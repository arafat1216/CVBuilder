using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateProjectRequest
{
    public class UpdateProjectRequestCommandHandler : IRequestHandler<UpdateProjectRequestCommand, UpdateProjectRequestCommandResponse>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public UpdateProjectRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, IProjectRepository projectRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.projectRepository = projectRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<UpdateProjectRequestCommandResponse> Handle(UpdateProjectRequestCommand request, CancellationToken cancellationToken)
        {
            // check if project exists
            var projectExists = await GetProjectExists(request.ProjectId);

            if (!projectExists)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            // create resource request
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var projectUpdateRequest = mapper.Map<ProjectUpdateRequest>(request);

            resourceRequest.ProjectUpdateRequest = projectUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<UpdateProjectRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(UpdateProjectRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Modify.ToString(),
                ResourceType = ResourceType.Project.ToString(),
                Reason = request.Reason
            };
        }

        private async Task<bool> GetProjectExists(int projectId)
        {
            return await projectRepository.ExistsAsync(applicationUser.GetUserId(), projectId);
        }
    }
}
