using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.ResourceRequests;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.ListAllRequests
{
    public class ListAllResourceRequestsQueryHandler : IRequestHandler<ListAllResourceRequestsQuery, List<ResourceRequestsListDto>>
    {
        private readonly IMapper mapper;
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;

        public ListAllResourceRequestsQueryHandler(IMapper mapper, IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser)
        {
            this.mapper = mapper;
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
        }

        

        public async Task<List<ResourceRequestsListDto>> Handle(ListAllResourceRequestsQuery request, CancellationToken cancellationToken)
        {
            var resourceRequestsList = await resourceRequestRepository.ListAllAsync(r => r.AppliedBy == applicationUser.GetUserId());

            return mapper.Map<List<ResourceRequestsListDto>>(resourceRequestsList);
        }
    }
}
