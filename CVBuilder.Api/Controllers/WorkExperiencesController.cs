using AutoMapper;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList;
using CVBuilder.Application.ViewModels.WorkExperience;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class WorkExperiencesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public WorkExperiencesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkExperiences(Guid employeeId)
        {
            var requestDto = new GetWorkExperiencesListQuery()
            {
                EmployeeId = employeeId,
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }

        [HttpGet("{workExperienceId}")]
        public async Task<IActionResult> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            var requestDto = new GetWorkExperienceDetailsQuery()
            {
                EmployeeId = employeeId,
                WorkExperienceId = workExperienceId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkExperience(Guid employeeId, WorkExperienceViewModel workExperienceViewModel)
        {

            var requestDto = mapper.Map<AddWorkExperienceCommand>(workExperienceViewModel);

            requestDto.EmployeeId = employeeId;

            var response = await mediator.Send(requestDto);

            return CreatedAtAction(nameof(GetWorkExperienceDetails), new { employeeId = response.EmployeeId, workExperienceId = response.WorkExperienceId }, response);

        }

        [HttpPut("{workExperienceId}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid employeeId, int workExperienceId, WorkExperienceViewModel workExperienceViewModel)
        {
            var requestDto = mapper.Map<UpdateWorkExperienceCommand>(workExperienceViewModel);

            requestDto.EmployeeId = employeeId;

            requestDto.WorkExperienceId = workExperienceId;

            await mediator.Send(requestDto);

            return Ok(requestDto);
        }

        [HttpDelete("{workExperienceId}")]
        public async Task<IActionResult> DeleteWorkExperience(Guid employeeId, int workExperienceId)
        {
            var requestDto = new DeleteWorkExperienceCommand()
            {
                EmployeeId = employeeId,
                WorkExperienceId = workExperienceId,
            };

            await mediator.Send(requestDto);

            return NoContent();
        }
    }
}
