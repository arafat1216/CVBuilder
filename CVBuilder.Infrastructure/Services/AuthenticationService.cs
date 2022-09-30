using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Exceptions;
using CVBuilder.Application.Validators;
using CVBuilder.Application.ViewModels.Login;

namespace CVBuilder.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository repository;
        private readonly ITokenGeneratorService tokenGeneratorService;

        public AuthenticationService(IEmployeeRepository repository, ITokenGeneratorService tokenGeneratorService)
        {
            this.repository = repository;
            this.tokenGeneratorService = tokenGeneratorService;
        }
        public async Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel)
        {
            #region validate incoming request

            var validator = new LoginValidator();

            var validationResult = await validator.ValidateAsync(loginViewModel);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            #endregion

            var user = await repository.GetEmployeeByEmailAsync(loginViewModel.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                throw new Exception("Invalid Credentials");

            var token = tokenGeneratorService.GenerateToken(user);

            return token;
        }
    }
}
