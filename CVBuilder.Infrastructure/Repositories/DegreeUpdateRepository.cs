using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class DegreeUpdateRepository : BaseRepository<DegreeUpdateRequest>, IDegreeUpdateRepository
    {
        public DegreeUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<DegreeUpdateRequest?> GetDegreeUpdateRequestByIdAsync(int requestId)
        {
            return await dbSet.Where(d => d.RequestId.Equals(requestId)).FirstOrDefaultAsync();
        }
    }
}
