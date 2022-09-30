using CVBuilder.Application.Contracts.Persistence;
using MediatR;
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

        public UpdatePasswordCommandHandler(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            #region fetch employee details

            var employee = await repository.GetEmployeeDetailsByIdAsync(request.EmployeeId);

            #endregion

            #region verify current password

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, employee.Password))
                throw new Exceptions.BadRequestException("Current Password Does Not Match");

            #endregion


            #region validate new password

            var validator = new UpdatePasswordCommandValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region hashing new password

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            #endregion

            employee.Password = hashedPassword;

            await repository.UpdateEmployeePasswordAsync(employee);

            return Unit.Value;
        }
    }
}
