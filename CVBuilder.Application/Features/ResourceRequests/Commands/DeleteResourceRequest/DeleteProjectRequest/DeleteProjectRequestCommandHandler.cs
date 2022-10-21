using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteProjectRequest
{
    public class DeleteProjectRequestCommandHandler : IRequestHandler<DeleteProjectRequestCommand, DeleteProjectRequestCommandResponse>
    {
        private readonly IProjectRepository projectRepository;
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public DeleteProjectRequestCommandHandler(IProjectRepository projectRepository, IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<DeleteProjectRequestCommandResponse> Handle(DeleteProjectRequestCommand request, CancellationToken cancellationToken)
        {
            // check if project exists
            var projectExists = await ProjectExists(request.ProjectId);

            if (!projectExists)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            ResourceRequest resourceRequest = CreateResourceRequest(request);

            ProjectUpdateRequest projectUpdateRequest = mapper.Map<ProjectUpdateRequest>(request);

            resourceRequest.ProjectUpdateRequest = projectUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<DeleteProjectRequestCommandResponse>(response);

        }

        private ResourceRequest CreateResourceRequest(DeleteProjectRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Remove.ToString(),
                ResourceType = ResourceType.Project.ToString(),
                Reason = request.Reason
            };

        }

        private async Task<bool> ProjectExists(int projectId)
        {
            return await projectRepository.ExistsAsync(applicationUser.GetUserId(), projectId);
        }
    }
}
