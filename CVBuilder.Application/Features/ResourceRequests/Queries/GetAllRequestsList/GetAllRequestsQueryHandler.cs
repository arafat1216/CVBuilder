using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.GetAllRequestsList
{
    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsListQuery, (List<ResourceRequestsListDto>, PaginationMetaData)>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IMapper mapper;

        public GetAllRequestsQueryHandler(IResourceRequestRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<(List<ResourceRequestsListDto>, PaginationMetaData)> Handle(GetAllRequestsListQuery request, CancellationToken cancellationToken)
        {
            var (response, metaData) = await repository.GetAllResourceRequestsAsync(request.PageNumber, request.PageSize, request.Status);

            var requestsListDto = mapper.Map<List<ResourceRequestsListDto>>(response);

            return (requestsListDto, metaData);
        }
    }
}
