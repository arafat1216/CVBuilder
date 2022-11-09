using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Contracts.UploadEmailToQueue;
using CVBuilder.Application.Dtos.Email;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreesList;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.Projects.Queries.GetProjectsList;
using CVBuilder.Application.Features.Skills.Queries.GetSkillsList;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList;
using CVBuilder.Application.ViewModels.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Basic")]
    public class MyCVController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPdfGeneratorService pdfGeneratorService;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;
        private readonly IUploadEmailToQueueService uploadEmailToQueueService;

        public MyCVController(IMediator mediator, IPdfGeneratorService pdfGeneratorService, IApplicationUser applicationUser, IMapper mapper, IUploadEmailToQueueService uploadEmailToQueueService)
        {
            this.mediator = mediator;
            this.pdfGeneratorService = pdfGeneratorService;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
            this.uploadEmailToQueueService = uploadEmailToQueueService;
            
        }


        [HttpGet("view-cv")]
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


        [HttpGet("degrees")]
        public async Task<IActionResult> GetAllDegrees()
        {
            var requestDto = new GetDegreesListQuery()
            {
                EmployeeId = applicationUser.GetUserId()
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }


        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var requestDto = new GetProjectsListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }


        [HttpGet("skills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var requestDto = new GetSkillsListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var dtos = await mediator.Send(requestDto);

            return Ok(dtos);
        }


        [HttpGet("work-experiences")]
        public async Task<IActionResult> GetAllWorkExperiences()
        {
            var requestDto = new GetWorkExperiencesListQuery()
            {
                EmployeeId = applicationUser.GetUserId(),
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }


        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailViewModel emailViewModel)
        {
            var employeeDetailsRequestDto = new GetEmployeeDetailQuery()
            {
                Id = applicationUser.GetUserId(),
            };

            var employeeDetails = await mediator.Send(employeeDetailsRequestDto);

            var requestDto = mapper.Map<EmailDto>(emailViewModel);

            requestDto.Id = applicationUser.GetUserId();

            requestDto.Sender = User.FindFirstValue(ClaimTypes.Email);

            var file = await pdfGeneratorService.GeneratePdf(employeeDetails);

            await uploadEmailToQueueService.UploadEmailToQueue(requestDto, file);

            return Ok("Sent Successfully");
        }
    }
}
