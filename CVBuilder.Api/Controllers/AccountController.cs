using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Exceptions;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.Features.UpdatePersonalDetails.Commands;
using CVBuilder.Application.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IPdfGeneratorService pdfGeneratorService;
        private readonly ITemplateGeneratorService templateGeneratorService;

        public AccountController(IAuthenticationService service, IMediator mediator, IMapper mapper, ILogger<AccountController> logger, IPdfGeneratorService pdfGeneratorService, ITemplateGeneratorService templateGeneratorService)
        {
            this.service = service;
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
            this.pdfGeneratorService = pdfGeneratorService;
            this.templateGeneratorService = templateGeneratorService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var token = await service.AuthenticateUserAsync(loginViewModel);

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

        [HttpGet("download-cv")]
        public async Task<IActionResult> DownloadCv()
        {
            var userId = Guid.Parse(User.Identity.Name);

            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = userId,
            };

            var employeeDetails = await mediator.Send(requestDto);

            var file = await pdfGeneratorService.GeneratePdf(employeeDetails);

            return File(file,"application/octet-stream","Resume.pdf");
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

        [HttpPatch("update-personal-details")]
        public async Task<IActionResult> UpdatePersonalDetails([FromBody] JsonPatchDocument document)
        {
            var requestDto = new UpdatePersonalDetailsCommand();

            document.ApplyTo(requestDto);   

            requestDto.EmployeeId = Guid.Parse(User.Identity.Name);

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }

    }
}
