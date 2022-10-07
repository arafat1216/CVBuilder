using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.DeleteSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.Features.Skills.Queries.GetSkillDetails;
using CVBuilder.Application.Features.Skills.Queries.GetSkillsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SkillsController(IMediator mediatR)
        {
            this.mediator = mediatR;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills(Guid employeeId)
        {
            var requestDto = new GetSkillsListQuery()
            {
                EmployeeId = employeeId,
            };

            var dtos = await mediator.Send(requestDto);

            return Ok(dtos);    
        }

        [HttpGet("{skillId}")]
        public async Task<IActionResult> GetSkillDetails(Guid employeeId, int skillId)
        {
            var requestDto = new GetSkillDetailsQuery()
            {
                EmployeeId = employeeId,
                SkillId = skillId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(Guid employeeId, AddSkillCommand addSkillCommand)
        {
            if (employeeId != addSkillCommand.EmployeeId)
                return BadRequest();

            var response = await mediator.Send(addSkillCommand);

           
            return CreatedAtAction(nameof(GetSkillDetails), new { employeeId = response.EmployeeId, skillId =response.SkillId }, response);

        }

        [HttpPut("{skillId}")]
        public async Task<IActionResult> UpdateSkill(Guid employeeId, int skillId, UpdateSkillCommand updateSkillCommand)
        {
            if ((skillId != updateSkillCommand.SkillId)||(employeeId != updateSkillCommand.EmployeeId))
                return BadRequest();

            await mediator.Send(updateSkillCommand);

            return NoContent();

        }

        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeleteSkill(Guid employeeId, int skillId)
        {
            var requestDto = new DeleteSkillCommand()
            {
                EmployeeId = employeeId,
                SkillId = skillId,
            };

            await mediator.Send(requestDto);

            return NoContent();
        }
    }
}
