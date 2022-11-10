using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.CVRequest;
using CVBuilder.Application.Features.Company.Commands.AddCompany;
using CVBuilder.Application.Features.Company.Commands.UpdateCompany;
using CVBuilder.Application.Features.Company.Commands.UpdateSubscription;
using CVBuilder.Application.Features.Company.Commands.UpdateSubscriptionStatus;
using CVBuilder.Application.Features.Company.Queries.GetCompaniesList;
using CVBuilder.Application.Features.Company.Queries.GetCompanyDetails;
using CVBuilder.Application.ViewModels.Company;
using CVBuilder.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Company")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IAuthenticationService authenticationService;
        private readonly ICVRequestService cVRequestService;

        public CompaniesController(IMapper mapper, IMediator mediator, IAuthenticationService authenticationService, ICVRequestService cVRequestService)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.authenticationService = authenticationService;
            this.cVRequestService = cVRequestService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var requestDto = mapper.Map<AddCompanyCommand>(registerViewModel);

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CompanyLoginViewModel companyLoginViewModel)
        {
            var token = await authenticationService.AuthenticateCompanyAsync(companyLoginViewModel);

            return Ok(token);
        }

        [HttpGet("company-details")]
        public async Task<IActionResult> CompanyDetails()
        {
            var requestDto = new GetCompanyDetailsQuery()
            {
                CompanyId = Guid.Parse(User.Identity.Name)
            };

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }

        [HttpGet("request-cv")]
        public async Task<IActionResult> RequestCV([FromQuery] string? searchBySkill, [FromQuery] string? searchByDegree, [FromQuery] string? searchByProject)
        {
            var viewModel = new CVRequestViewModel()
            {
                CompanyId = Guid.Parse(User.Identity.Name),
                SearchBySkill = searchBySkill,
                searchByDegree = searchByDegree,
                searchByProject = searchByProject,

            };

            var response = await cVRequestService.RequestCV(viewModel);

            return Ok(response);
        }

        [HttpPut("update-company-details")]
        public async Task<IActionResult> UpdateCompanyDetails([FromBody] UpdateCompanyDetailsViewModel viewModel)
        {
            var requestDto = mapper.Map<UpdateCompanyDetailsCommand>(viewModel);

            requestDto.CompanyId = Guid.Parse(User.Identity.Name);

            await mediator.Send(requestDto);

            return Ok(requestDto);

        }

        [HttpPut("update-subscription")]
        public async Task<IActionResult> UpdateSubscription([FromBody] SubscriptionType subscriptionType )
        {
            var requestDto = new UpdateSubscriptionCommand()
            {
                CompanyId = Guid.Parse(User.Identity.Name),
                SubscriptionType = subscriptionType

            };

            await mediator.Send(requestDto);

            return Ok("Updated Successfully");

        }


    }
}
