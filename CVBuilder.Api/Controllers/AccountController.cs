using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.ViewModels.Login;
using MediatR;
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
            if (loginViewModel.Email == null || loginViewModel.Password == null)
                return BadRequest();

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
            var userId = Guid.Parse(User.Identity.Name);

            var requestDto = new UpdatePasswordCommand()
            {
                EmployeeId = userId,
                CurrentPassword = updatePasswordViewModel.CurrentPassword,
                NewPassword = updatePasswordViewModel.NewPassword,
                ConfirmPassword = updatePasswordViewModel.ConfirmPassword
            };

            await mediator.Send(requestDto);

            return NoContent();
        }

        
    }
}
