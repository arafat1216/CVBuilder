using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IDegreeRepository : IAsyncRepository<Degree>
    {
        Task<IReadOnlyList<Degree>> ListAllAsync(Guid employeeId);
        Task<Degree?> GetSkillByIdAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);
    }
}
