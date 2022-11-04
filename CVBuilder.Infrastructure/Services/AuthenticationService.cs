using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Exceptions;
using CVBuilder.Application.Validators;
using CVBuilder.Application.ViewModels.Account;
using CVBuilder.Application.ViewModels.Company;

namespace CVBuilder.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ITokenGeneratorService tokenGeneratorService;
        private readonly ICompanyRepository companyRepository;

        public AuthenticationService(IEmployeeRepository employeeRepository, ITokenGeneratorService tokenGeneratorService, ICompanyRepository companyRepository)
        {
            this.employeeRepository = employeeRepository;
            this.tokenGeneratorService = tokenGeneratorService;
            this.companyRepository = companyRepository;
        }

        public async Task<string> AuthenticateCompanyAsync(CompanyLoginViewModel companyLoginViewModel)
        {
            var company = await companyRepository.GetCompanyByNameAsync(companyLoginViewModel.UserName);

            if (company == null || !BCrypt.Net.BCrypt.Verify(companyLoginViewModel.Password, company.Password))
                throw new UnAuthorizedException("Invalid Credentials");

            var token = tokenGeneratorService.GenerateToken(company);

            return token;
        }

        public async Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel)
        {
            
            var user = await employeeRepository.GetEmployeeByEmailAsync(loginViewModel.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
                throw new UnAuthorizedException("Invalid Credentials");

            var token = tokenGeneratorService.GenerateToken(user);

            return token;
        }
    }
}
