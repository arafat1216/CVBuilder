using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class ProjectUpdateRepository : BaseRepository<ProjectUpdateRequest>, IProjectUpdateRepository
    {
        public ProjectUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ProjectUpdateRequest?> GetProjectUpdateRequestByIdAsync(int requestId)
        {
            return await dbSet.Where(p => p.RequestId.Equals(requestId)).FirstOrDefaultAsync();
        }
    }
}
