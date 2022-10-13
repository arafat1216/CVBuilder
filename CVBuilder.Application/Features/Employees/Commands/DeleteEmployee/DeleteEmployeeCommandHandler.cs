using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {

            // fetch employee 

            var employee = await GetEmployee(request.EmployeeId);



            // check if employee exists

            if (employee == null)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            
            if (request.SoftDelete)
            {
                employee.IsDeleted = true;

                await repository.UpdateEmployeeAsync(employee);

                return Unit.Value;
            }


            await repository.DeleteEmployeeAsync(employee);

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployee(Guid employeeId)
        {
            return await repository.GetEmployeeByIdAsync(employeeId);
        }
    }
}
