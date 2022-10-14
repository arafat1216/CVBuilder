using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee
{
    public class PartialUpdateEmployeeCommandHandler : IRequestHandler<PartialUpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PartialUpdateEmployeeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public PartialUpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper, ILogger<PartialUpdateEmployeeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(PartialUpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeDetails = await GetEmployeeDetails(request.EmployeeId);

            if (employeeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);


            mapper.Map(request, employeeDetails);

            await repository.UpdateEmployeeAsync(employeeDetails);

            logger.LogInformation($"Employee With Id: {request.EmployeeId} Updated By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployeeDetails(Guid employeeId)
        {
            return await repository.GetEmployeeDetailsAsync(employeeId);
        }
    }
}
