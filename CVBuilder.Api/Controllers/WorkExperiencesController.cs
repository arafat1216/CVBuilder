using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails;
using CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    public class WorkExperiencesController : ControllerBase
    {
        private readonly IMediator mediator;

        public WorkExperiencesController(IMediator mediator)
        {
            this.mediator = mediator;
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
        public async Task<IActionResult> AddWorkExperience(Guid employeeId, AddWorkExperienceCommand addWorkExperienceCommand)
        {
            if (employeeId != addWorkExperienceCommand.EmployeeId)
                return BadRequest();

            var response = await mediator.Send(addWorkExperienceCommand);

            return CreatedAtAction(nameof(GetWorkExperienceDetails), new { employeeId = response.EmployeeId, workExperienceId = response.WorkExperienceId }, response);
        
        }

        [HttpPut("{workExperienceId}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid employeeId, int workExperienceId, UpdateWorkExperienceCommand updateWorkExperienceCommand)
        {
            if ((employeeId != updateWorkExperienceCommand.EmployeeId) || (workExperienceId != updateWorkExperienceCommand.WorkExperienceId))
                return BadRequest();

            await mediator.Send(updateWorkExperienceCommand);

            return NoContent();
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
