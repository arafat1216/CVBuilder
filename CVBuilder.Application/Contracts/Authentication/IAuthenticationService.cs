﻿using CVBuilder.Application.ViewModels.Login;

namespace CVBuilder.Application.Contracts.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(LoginViewModel loginViewModel);
    }
}
