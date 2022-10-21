using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteWorkExperienceRequest
{
    public class DeleteWorkExperienceRequestCommandHandler : IRequestHandler<DeleteWorkExperienceRequestCommand, DeleteWorkExperienceRequestCommandResponse>
    {
        private readonly IWorkExperienceRepository workExperienceRepository;
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public DeleteWorkExperienceRequestCommandHandler(IWorkExperienceRepository workExperienceRepository, IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.workExperienceRepository = workExperienceRepository;
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }
        public async Task<DeleteWorkExperienceRequestCommandResponse> Handle(DeleteWorkExperienceRequestCommand request, CancellationToken cancellationToken)
        {
            // check if work experience exists
            bool workExperienceExists = await WorkExperineceExists(request.WorkExperienceId);

            ResourceRequest resourceRequest = CreateRequest(request);

            WorkExperienceUpdateRequest workExperienceUpdateRequest = mapper.Map<WorkExperienceUpdateRequest>(request);

            resourceRequest.WorkExperienceUpdateRequest = workExperienceUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<DeleteWorkExperienceRequestCommandResponse>(response);
        }

        private ResourceRequest CreateRequest(DeleteWorkExperienceRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Remove.ToString(),
                ResourceType = ResourceType.WorkExperience.ToString(),
                Reason = request.Reason
            };
        }

        private Task<bool> WorkExperineceExists(int workExperienceId)
        {
            return workExperienceRepository.ExistsAsync(applicationUser.GetUserId(), workExperienceId);
        }
    }
}
