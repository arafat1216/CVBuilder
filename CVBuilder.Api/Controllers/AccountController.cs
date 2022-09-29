using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService service;

        public AccountController(IAuthenticationService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Email == null || loginViewModel.Password == null)
                return BadRequest();

            var token = await service.AuthenticateUserAsync(loginViewModel);

            if (token == null)
                return NotFound();

            return Ok(token);
        }

        
    }
}
