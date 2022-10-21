using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteSkillRequest
{
    public class DeleteSkillRequestCommandHandler : IRequestHandler<DeleteSkillRequestCommand, DeleteSkillRequestCommandResponse>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public DeleteSkillRequestCommandHandler(ISkillRepository skillRepository, IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<DeleteSkillRequestCommandResponse> Handle(DeleteSkillRequestCommand request, CancellationToken cancellationToken)
        {
            // check if skill exists
            bool skillExists = await SkillExists(request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            ResourceRequest resourceRequest = CreateRequest(request);

            SkillUpdateRequest skillUpdateRequest = mapper.Map<SkillUpdateRequest>(request);

            resourceRequest.SkillUpdateRequest = skillUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<DeleteSkillRequestCommandResponse>(response);
        }

        private ResourceRequest CreateRequest(DeleteSkillRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Remove.ToString(),
                ResourceType = ResourceType.Skill.ToString(),
                Reason = request.Reason
            };
        }

        private async Task<bool> SkillExists(int skillId)
        {
            return await skillRepository.ExistsAsync(applicationUser.GetUserId(), skillId);
        }
    }
}
