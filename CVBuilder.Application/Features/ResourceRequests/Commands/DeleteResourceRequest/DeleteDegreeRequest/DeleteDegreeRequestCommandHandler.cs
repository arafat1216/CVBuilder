using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteDegreeRequest
{
    public class DeleteDegreeRequestCommandHandler : IRequestHandler<DeleteDegreeRequestCommand, DeleteDegreeRequestCommandResponse>
    {
        private readonly IDegreeRepository degreeRepository;
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public DeleteDegreeRequestCommandHandler(IDegreeRepository degreeRepository, IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.degreeRepository = degreeRepository;
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<DeleteDegreeRequestCommandResponse> Handle(DeleteDegreeRequestCommand request, CancellationToken cancellationToken)
        {
            // check if degree exists
            var degreeExists = await DegreeExists(request.DegreeId);

            if (!degreeExists)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            ResourceRequest resourceRequest = CreateRequest(request);

            DegreeUpdateRequest degreeUpdateRequest = mapper.Map<DegreeUpdateRequest>(request);

            resourceRequest.DegreeUpdateRequest = degreeUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<DeleteDegreeRequestCommandResponse>(response);
        }

        private async Task<bool> DegreeExists(int degreeId)
        {
            return await degreeRepository.ExistsAsync(applicationUser.GetUserId(), degreeId);
        }

        private ResourceRequest CreateRequest(DeleteDegreeRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Remove.ToString(),
                ResourceType = ResourceType.Degree.ToString(),
                Reason = request.Reason
            };
        }
    }
}
