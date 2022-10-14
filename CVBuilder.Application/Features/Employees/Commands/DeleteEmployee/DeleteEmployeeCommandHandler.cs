using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly ILogger<DeleteEmployeeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository, ILogger<DeleteEmployeeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.logger = logger;
            this.applicationUser = applicationUser;
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

                logger.LogInformation($"Employee With Id: {request.EmployeeId} Sofy Deleted By {applicationUser.GetUserId()}");

                return Unit.Value;
            }


            await repository.DeleteEmployeeAsync(employee);

            logger.LogInformation($"Employee With Id: {request.EmployeeId} Hard Deleted By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployee(Guid employeeId)
        {
            return await repository.GetEmployeeByIdAsync(employeeId);
        }
    }
}
