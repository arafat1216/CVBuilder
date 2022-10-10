using CVBuilder.Application.ViewModels.Account;

namespace CVBuilder.Application.Contracts.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel);
    }
}
