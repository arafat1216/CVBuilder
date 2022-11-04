using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class CVRequestRepository : BaseRepository<Domain.Entities.CVRequest>, ICVRequestRepository
    {
        public CVRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CVRequest?> GetCVRequest(Guid companyId, string currentDate)
        {
            return await dbSet.Where(c => c.CompanyId == companyId && c.Date == currentDate).FirstOrDefaultAsync();
        }
    }
}
