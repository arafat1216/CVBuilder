using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee
{
    public class PartialUpdateEmployeeCommandHandler : IRequestHandler<PartialUpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;

        public PartialUpdateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(PartialUpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeDetails = await GetEmployeeDetails(request.EmployeeId);

            if (employeeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            employeeDetails.FullName = request.FullName ?? employeeDetails.FullName;

            employeeDetails.Address = request.Address ?? employeeDetails.Address;

            employeeDetails.PhoneNo = request.PhoneNo ?? employeeDetails.PhoneNo;

            employeeDetails.Email = request.Email ?? employeeDetails.Email;

            employeeDetails.Role = request.Role ?? employeeDetails.Role;

            await repository.UpdateEmployeePartiallyAsync(employeeDetails);

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployeeDetails(Guid employeeId)
        {
            return await repository.GetEmployeeDetailsAsync(employeeId);
        }
    }
}
