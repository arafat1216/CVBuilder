using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreesList;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.Projects.Queries.GetProjectsList;
using CVBuilder.Application.Features.Skills.Queries.GetSkillsList;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MyCVController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPdfGeneratorService pdfGeneratorService;
        private readonly IApplicationUser applicationUser;

        public MyCVController(IMediator mediator, IPdfGeneratorService pdfGeneratorService, IApplicationUser applicationUser)
        {
            this.mediator = mediator;
            this.pdfGeneratorService = pdfGeneratorService;
            this.applicationUser = applicationUser;
        }


        [HttpGet("view-my-cv")]
        public async Task<IActionResult> GetMyCv()
        {
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = applicationUser.GetUserId(),
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }


        [HttpGet("download-cv")]
        public async Task<IActionResult> DownloadCv()
        {
           
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = applicationUser.GetUserId(),
            };

            var employeeDetails = await mediator.Send(requestDto);

            var file = await pdfGeneratorService.GeneratePdf(employeeDetails);

            return File(file, "application/octet-stream", "Resume.pdf");
        }


        [HttpGet("degrees-list")]
        public async Task<IActionResult> GetAllDegrees()
        {
            var requestDto = new GetDegreesListQuery()
            {
                EmployeeId = applicationUser.GetUserId()
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }


        [HttpGet("projects-list")]
        public async Task<IActionResult> GetAllProjects()
        {
            var requestDto = new GetProjectsListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }


        [HttpGet("skills-list")]
        public async Task<IActionResult> GetAllSkills()
        {
            var requestDto = new GetSkillsListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var dtos = await mediator.Send(requestDto);

            return Ok(dtos);
        }


        [HttpGet("work-experiences-list")]
        public async Task<IActionResult> GetAllWorkExperiences()
        {
            var requestDto = new GetWorkExperiencesListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }
    }
}
