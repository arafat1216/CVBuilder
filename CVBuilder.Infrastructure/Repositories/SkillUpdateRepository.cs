using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;

namespace CVBuilder.Infrastructure.Repositories
{
    public class SkillUpdateRepository : BaseRepository<SkillUpdateRequest>, ISkillUpdateRepository
    {
        public SkillUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
