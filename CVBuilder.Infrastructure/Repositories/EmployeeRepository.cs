using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);

            await context.SaveChangesAsync();

            return employee;
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            context.Employees.Remove(employee);

            await context.SaveChangesAsync();
        }

        public async Task<bool> EmployeeExistsAsync(Guid employeeId)
        {
            return await context.Employees.AnyAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await context.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            context.Employees.Update(employee);
            
            await context.SaveChangesAsync();
        }
    }
}
