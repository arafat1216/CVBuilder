using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IDegreeUpdateRepository : IAsyncRepository<DegreeUpdateRequest>
    {
        Task<DegreeUpdateRequest?> GetDegreeUpdateRequestByIdAsync(int requestId);
    }
}
