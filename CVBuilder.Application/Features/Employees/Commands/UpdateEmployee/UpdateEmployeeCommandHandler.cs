using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployeeToUpdate(Guid employeeId)
        {
            return await repository.GetEmployeeDetailsAsync(employeeId);
        }

    }
}
