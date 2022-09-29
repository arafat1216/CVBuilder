using CVBuilder.Application.ViewModels;

namespace CVBuilder.Application.Contracts.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel);
    }
}
