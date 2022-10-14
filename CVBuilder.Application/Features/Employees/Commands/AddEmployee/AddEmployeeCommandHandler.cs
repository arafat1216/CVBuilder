using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<AddEmployeeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public AddEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper, ILogger<AddEmployeeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<AddEmployeeCommandResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {

            // mapping incoming request to employee entity

            var employee = mapper.Map<Employee>(request);



            // hashing password

            employee.Password = GetHashedPassword(request.Password);
            

            employee = await repository.AddEmployeeAsync(employee);

            var response = mapper.Map<AddEmployeeCommandResponse>(employee);

            logger.LogInformation($"Employee With Id: {response.EmployeeId} Added By {applicationUser.GetUserId()}");

            return response;
        }

        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
