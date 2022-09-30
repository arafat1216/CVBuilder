using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IDegreeRepository : IAsyncRepository<Degree>
    {
        Task<Degree?> GetDegreeByIdAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);
    }
}
