using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface ISkillRepository : IAsyncRepository<Skill> 
    {
        Task<Skill?> GetSkillByIdAsync(Guid employeeId, int id);
        Task<bool> ExistsAsync(Guid employeeId, int id);
    }
}
