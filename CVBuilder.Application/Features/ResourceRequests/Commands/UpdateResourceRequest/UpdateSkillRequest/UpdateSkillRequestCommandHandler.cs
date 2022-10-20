using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateSkillRequest
{
    public class UpdateSkillRequestCommandHandler : IRequestHandler<UpdateSkillRequestCommand, UpdateSkillRequestCommandResponse>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly ISkillRepository skillRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public UpdateSkillRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, ISkillRepository skillRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.skillRepository = skillRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<UpdateSkillRequestCommandResponse> Handle(UpdateSkillRequestCommand request, CancellationToken cancellationToken)
        {
            // check if skill exists
            bool skillExists = await GetskillExists(request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            // create new resource request
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var skillUpdateRequest = mapper.Map<SkillUpdateRequest>(request);

            resourceRequest.SkillUpdateRequest = skillUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<UpdateSkillRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(UpdateSkillRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Modify.ToString(),
                ResourceType = ResourceType.Skill.ToString(),
                Reason = request.Reason
            };
        }

        private async Task<bool> GetskillExists(int skillId)
        {
            return await skillRepository.ExistsAsync(applicationUser.GetUserId(), skillId);
        }
    }
}
