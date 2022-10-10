using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CVBuilder.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> predicate)
        {
            var query = dbSet.AsQueryable();

            return await query.Where(predicate).ToListAsync<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
