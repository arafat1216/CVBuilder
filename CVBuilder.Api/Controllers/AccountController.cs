using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Exceptions;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAuthenticationService service, IMediator mediator, IMapper mapper, ILogger<AccountController> logger)
        {
            this.service = service;
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var token = await service.AuthenticateUserAsync(loginViewModel);

            logger.LogInformation($"Logged in at {DateTime.Now}");

            return Ok(token);
        }

        [HttpGet("view-cv")]
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

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordViewModel updatePasswordViewModel)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var requestDto = mapper.Map<UpdatePasswordCommand>(updatePasswordViewModel);

            requestDto.Email = userEmail;

            try
            {
                await mediator.Send(requestDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Password Update Failed: {ex.Message}");
                throw new BadRequestException("Can Not Update Password");
            }

            logger.LogInformation($"Password Successfully Updated");

            return NoContent();
        }


    }
}
