using CVBuilder.Application.Models.Pagination;
using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IResourceRequestRepository : IAsyncRepository<ResourceRequest>
    {
        Task<ResourceRequest?> GetResourceRequestByIdAsync(int id);
        Task<ResourceRequest?> GetResourceRequestDetailsAsync(int id);
        Task<(IReadOnlyList<ResourceRequest>, PaginationMetaData)> GetAllResourceRequestsAsync(int pageNumber, int pageSize, string? status);
        Task<(IReadOnlyList<ResourceRequest>, PaginationMetaData)> GetAllResourceRequestsAsync(Guid employeeId, int pageNumber, int pageSize, string? status);



    }
}
