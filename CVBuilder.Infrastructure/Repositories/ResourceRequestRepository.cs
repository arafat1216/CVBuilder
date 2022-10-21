using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Models.Pagination;
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

        public async Task<(IReadOnlyList<ResourceRequest>, PaginationMetaData)> GetAllResourceRequestsAsync(int pageNumber, int pageSize, string? status)
        {
            var collection = dbSet as IQueryable<ResourceRequest>;

            if (!string.IsNullOrEmpty(status))
            {
                status = status.Trim();

                collection = collection.Where(x => x.Status == status);
            }

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<(IReadOnlyList<ResourceRequest>, PaginationMetaData)> GetAllResourceRequestsAsync(Guid employeeId, int pageNumber, int pageSize, string? status)
        {
            var collection = dbSet as IQueryable<ResourceRequest>;

            collection = collection.Where(r => r.AppliedBy.Equals(employeeId));

            if (!string.IsNullOrEmpty(status))
            {
                status = status.Trim();

                collection = collection.Where(r => r.Status == status);
            }

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<ResourceRequest?> GetResourceRequestByIdAsync(int id)
        {
            return await dbSet.Where(r => r.RequestId.Equals(id)).FirstOrDefaultAsync();
        }

        public Task<ResourceRequest?> GetResourceRequestDetailsAsync(int id)
        {
            return dbSet
                .Include(r => r.PersonalDetailsUpdateRequest)
                .Include(r => r.DegreeUpdateRequest)
                .Include(r => r.ProjectUpdateRequest)
                .Include(r => r.SkillUpdateRequest)
                .Include(r => r.WorkExperienceUpdateRequest)
                .Where(r => r.RequestId.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
