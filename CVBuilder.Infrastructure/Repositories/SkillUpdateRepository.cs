using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class SkillUpdateRepository : BaseRepository<SkillUpdateRequest>, ISkillUpdateRepository
    {
        public SkillUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SkillUpdateRequest?> GetSkillUpdateRequestByIdAsync(int requestId)
        {
            return await dbSet.Where(s => s.RequestId.Equals(requestId)).FirstOrDefaultAsync();
        }
    }
}
