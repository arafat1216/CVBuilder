using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;

namespace CVBuilder.Infrastructure.Repositories
{
    public class DegreeUpdateRepository : BaseRepository<DegreeUpdateRequest>, IDegreeUpdateRepository
    {
        public DegreeUpdateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
