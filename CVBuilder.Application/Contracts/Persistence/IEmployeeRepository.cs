﻿using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<Employee?> GetEmployeeDetailsAsync(Guid employeeId);
        Task UpdateEmployeeAsync(Employee employee);
        Task UpdateEmployeePartiallyAsync(Employee employee);
        Task UpdateEmployeePasswordAsync(Employee employee);    
        Task DeleteEmployeeAsync(Employee employee);
        Task<bool> EmployeeExistsAsync(Guid employeeId);
    }
}
