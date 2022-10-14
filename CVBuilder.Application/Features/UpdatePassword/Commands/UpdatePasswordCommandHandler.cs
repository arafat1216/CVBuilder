using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Features.UpdatePassword.Commands
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly ILogger<UpdatePasswordCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public UpdatePasswordCommandHandler(IEmployeeRepository repository, ILogger<UpdatePasswordCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            // fetch employee details

            var employee = await GetEmployee(request.Email);


            // verify current password

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, employee.Password))
                throw new Exceptions.BadRequestException("Current Password Does Not Match");



            // hashing new password

            employee.Password = GetHashedPassword(request.NewPassword);

            await repository.UpdateEmployeePasswordAsync(employee);

            logger.LogInformation($"Password Updated  By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private async Task<Employee?> GetEmployee(string email)
        {
            return await repository.GetEmployeeByEmailAsync(email);
        }
    }
}
