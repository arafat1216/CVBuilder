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
            
            var employee = await repository.GetEmployeeByIdAsync(request.EmployeeId);

            // Check if employee exists
            if (employee == null)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            await repository.DeleteEmployeeAsync(employee);
            
            return Unit.Value;
        }
    }
}
