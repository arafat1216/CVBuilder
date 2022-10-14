using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Models.Pagination;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Employee> dbset;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbset = context.Set<Employee>();
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await dbset.AddAsync(employee);

            await context.SaveChangesAsync();

            return employee;
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            dbset.Remove(employee);

            await context.SaveChangesAsync();
        }

        public async Task<bool> EmployeeExistsAsync(Guid employeeId)
        {
            return await dbset.AnyAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var collection = dbset as IQueryable<Employee>;

            collection = collection.Where(e => !e.IsDeleted);

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .OrderBy(e => e.FullName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);

        }

        public async Task<(List<Employee>, PaginationMetaData)> GetAllEmployeesCVAsync(string? searchBySkill, string? searchByDegree, int pageNumber, int pageSize)
        {
            var collection = dbset as IQueryable<Employee>;
            collection = collection
                .Include(e => e.Skills.Where(s => !s.IsDeleted))
                .Include(e => e.Degrees.Where(d => !d.IsDeleted))
                .Include(e => e.WorkExperiences.Where(w => !w.IsDeleted))
                .Include(e => e.Projects.Where(p => !p.IsDeleted))
                .Where(e => !e.IsDeleted);

            if (!string.IsNullOrEmpty(searchBySkill))
            {
                searchBySkill = searchBySkill.Trim();

                collection = collection.Where(e => e.Skills.Any(s => s.Name == searchBySkill && !s.IsDeleted));

            }

            if (!string.IsNullOrEmpty(searchByDegree))
            {
                searchByDegree = searchByDegree.Trim();

                collection = collection.Where(e => e.Degrees.Any(d => d.Subject.Equals(searchByDegree)));
            }

            var totalItems = await collection.CountAsync();

            var paginationMetaData = new PaginationMetaData(totalItems, pageNumber, pageSize);

            var collectionToReturn = await collection
                .OrderBy(e => e.FullName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await dbset.Where(e => e.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await dbset
                .Include(e => e.Skills.Where(s => !s.IsDeleted))
                .Include(e => e.Degrees.Where(d => !d.IsDeleted))
                .Include(e => e.WorkExperiences.Where(w => !w.IsDeleted))
                .Include(e => e.Projects.Where(p => !p.IsDeleted))
                .Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task<Employee?> GetEmployeeDetailsAsync(Guid employeeId)
        {
            return await dbset.Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            dbset.Update(employee);

            await context.SaveChangesAsync();
        }


        public async Task UpdateEmployeePasswordAsync(Employee employee)
        {
            dbset.Update(employee);

            await context.SaveChangesAsync();
        }
    }
}
