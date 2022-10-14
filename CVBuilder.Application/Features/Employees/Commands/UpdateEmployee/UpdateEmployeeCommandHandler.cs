using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateEmployeeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper, ILogger<UpdateEmployeeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            // check if employee exists

            var employeeToUpdate = await GetEmployeeToUpdate(request.EmployeeId);

            if (employeeToUpdate == null)
            {
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);
            }


            // mapping incoming request to employee

            mapper.Map(request, employeeToUpdate);


            await repository.UpdateEmployeeAsync(employeeToUpdate);

            logger.LogInformation($"Employee With Id: {request.EmployeeId} Updated By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployeeToUpdate(Guid employeeId)
        {
            return await repository.GetEmployeeDetailsAsync(employeeId);
        }

    }
}
