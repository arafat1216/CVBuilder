using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.UpdatePersonalDetails.Commands
{
    public class UpdatePersonalDetailsCommandHandler : IRequestHandler<UpdatePersonalDetailsCommand, UpdatePersonalDetailsCommandResponse>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IPersonalDetailsUpdateRepository personalDetailsUpdateRepository;
        private readonly IMapper mapper;

        public UpdatePersonalDetailsCommandHandler(IResourceRequestRepository resourceRequestRepository, IPersonalDetailsUpdateRepository personalDetailsUpdateRepository, IMapper mapper)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.personalDetailsUpdateRepository = personalDetailsUpdateRepository;
            this.mapper = mapper;
        }

        public async Task<UpdatePersonalDetailsCommandResponse> Handle(UpdatePersonalDetailsCommand request, CancellationToken cancellationToken)
        {
            // Generate A Resource Request
            var resourceRequestDto = new ResourceRequest()
            {
                AppliedBy = request.EmployeeId,
                RequestType = RequestType.Modify.ToString(),
                ResourceType = ResourceType.PersonalDetails.ToString(),
                Reason = request.Reason,
            };
          

            var personalDetailsUpdateRequest = mapper.Map<PersonalDetailsUpdateRequest>(request);

            resourceRequestDto.PersonalDetailsUpdateRequest = personalDetailsUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequestDto);


            return mapper.Map<UpdatePersonalDetailsCommandResponse>(response);
        }

        
    }
}
