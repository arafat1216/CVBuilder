using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class ResourceRequestRepository : BaseRepository<ResourceRequest>, IResourceRequestRepository
    {
        public ResourceRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ResourceRequest?> GetResourceRequestByIdAsync(int id)
        {
            return await dbSet.Where(r => r.RequestId == id).FirstOrDefaultAsync();
        }
    }
}
