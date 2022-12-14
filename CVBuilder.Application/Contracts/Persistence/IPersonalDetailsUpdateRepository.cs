using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IPersonalDetailsUpdateRepository : IAsyncRepository<PersonalDetailsUpdateRequest>
    {
        Task<PersonalDetailsUpdateRequest?> GetPersonalDetailsUpdateRequestByIdAsync(int requestId);
    }
}
