using CVBuilder.Application.ViewModels.Account;
using CVBuilder.Application.ViewModels.Company;

namespace CVBuilder.Application.Contracts.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel);
        Task<string> AuthenticateCompanyAsync(CompanyLoginViewModel companyLoginViewModel);
    }
}
