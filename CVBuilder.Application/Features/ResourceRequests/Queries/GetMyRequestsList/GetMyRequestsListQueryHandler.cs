using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.GetMyRequestsList
{
    public class GetMyRequestsListQueryHandler : IRequestHandler<GetMyRequestsListQuery, (List<ResourceRequestsListDto>, PaginationMetaData)>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IMapper mapper;
        private readonly IApplicationUser applicationUser;

        public GetMyRequestsListQueryHandler(IResourceRequestRepository repository, IMapper mapper, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.applicationUser = applicationUser;
        }

        public async Task<(List<ResourceRequestsListDto>, PaginationMetaData)> Handle(GetMyRequestsListQuery request, CancellationToken cancellationToken)
        {
            var (collection, metadata) = await repository.GetAllResourceRequestsAsync(applicationUser.GetUserId(), request.PageNumber, request.PageSize, request.Status);

            var resourcesListDto = mapper.Map<List<ResourceRequestsListDto>>(collection);

            return (resourcesListDto, metadata);
        }
    }
}

