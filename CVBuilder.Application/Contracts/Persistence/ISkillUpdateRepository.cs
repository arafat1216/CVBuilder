using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface ISkillUpdateRepository : IAsyncRepository<SkillUpdateRequest>
    {
    }
}
