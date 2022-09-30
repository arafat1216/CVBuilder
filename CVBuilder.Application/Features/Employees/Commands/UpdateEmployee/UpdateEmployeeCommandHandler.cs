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
            
            #region check if employee exists

            var employeeExists = await repository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
            {
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);
            }

            #endregion


            #region validate incoming request

            var validator = new UpdateEmployeeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            #endregion


            #region mapping incoming request to employee

            var employee = mapper.Map<Employee>(request);

            #endregion


            await repository.UpdateEmployeeAsync(employee);

            return Unit.Value;
        }
    }
}
