using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.ViewModels.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService service;
        private readonly IMediator mediator;

        public AccountController(IAuthenticationService service, IMediator mediator)
        {
            this.service = service;
            this.mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var token = await service.AuthenticateUserAsync(loginViewModel);

            return Ok(token);
        }

        [HttpGet("viewcv")]
        public async Task<IActionResult> GetMyCv()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = userId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost("updatepassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel updatePasswordViewModel)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var requestDto = new UpdatePasswordCommand()
            {
                Email = userEmail,
                CurrentPassword = updatePasswordViewModel.CurrentPassword,
                NewPassword = updatePasswordViewModel.NewPassword,
                ConfirmPassword = updatePasswordViewModel.ConfirmPassword
            };

            await mediator.Send(requestDto);

            return NoContent();
        }


    }
}
