using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IResourceRequestRepository : IAsyncRepository<ResourceRequest>
    {
        Task<ResourceRequest?> GetResourceRequestByIdAsync(int id);

    }
}
