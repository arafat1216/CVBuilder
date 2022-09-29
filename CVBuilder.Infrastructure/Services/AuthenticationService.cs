using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;

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
            var user = await repository.GetEmployeeByEmailAsync(loginViewModel.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                return null;

            var token = tokenGeneratorService.GenerateToken(user);

            return token;
        }
    }
}
