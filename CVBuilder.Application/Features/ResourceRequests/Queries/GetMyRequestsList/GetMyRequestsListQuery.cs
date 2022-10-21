using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.GetMyRequestsList
{
    public class GetMyRequestsListQuery : IRequest<(List<ResourceRequestsListDto>, PaginationMetaData)>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Status { get; set; }
    }
}
