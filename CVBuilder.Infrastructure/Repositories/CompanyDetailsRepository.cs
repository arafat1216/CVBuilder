using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class CompanyDetailsRepository : BaseRepository<CompanyDetails>, ICompanyDetailsRepository
    {
        public CompanyDetailsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CompanyDetails?> GetCompanyDetailsById(Guid companyId)
        {
            return await dbSet.Where(c => c.CompanyId == companyId).FirstOrDefaultAsync();
        }
    }
}
