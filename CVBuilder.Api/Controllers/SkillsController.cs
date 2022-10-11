using AutoMapper;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.DeleteSkill;
using CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.Features.Skills.Queries.GetSkillDetails;
using CVBuilder.Application.Features.Skills.Queries.GetSkillsList;
using CVBuilder.Application.ViewModels.Skill;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SkillsController(IMediator mediatR, IMapper mapper)
        {
            this.mediator = mediatR;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills([FromRoute] Guid employeeId)
        {
            var requestDto = new GetSkillsListQuery()
            {
                EmployeeId = employeeId,
            };

            var dtos = await mediator.Send(requestDto);

            return Ok(dtos);
        }

        [HttpGet("{skillId}")]
        public async Task<IActionResult> GetSkillDetails([FromRoute] Guid employeeId, [FromRoute] int skillId)
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
        public async Task<IActionResult> AddSkill([FromRoute] Guid employeeId, [FromBody] SkillViewModel skillViewModel)
        {

            var requestDto = mapper.Map<AddSkillCommand>(skillViewModel);

            requestDto.EmployeeId = employeeId;

            var response = await mediator.Send(requestDto);


            return CreatedAtAction(nameof(GetSkillDetails), new { employeeId = response.EmployeeId, skillId = response.SkillId }, response);

        }

        [HttpPut("{skillId}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid employeeId, [FromRoute] int skillId, [FromBody]SkillViewModel skillViewModel)
        {

            var requestDto = mapper.Map<UpdateSkillCommand>(skillViewModel);

            requestDto.SkillId = skillId;

            requestDto.EmployeeId = employeeId;

            await mediator.Send(requestDto);

            return Ok(requestDto);

        }


        [HttpPatch("{skillId}")]
        public async Task<IActionResult> UpdateSkillPartially([FromRoute] Guid employeeId, [FromRoute] int skillId, [FromBody] JsonPatchDocument patchDocument)
        {

            var partialUpdateSkillCommand = new PartialUpdateSkillCommand();

            patchDocument.ApplyTo(partialUpdateSkillCommand);

            partialUpdateSkillCommand.EmployeeId = employeeId;

            partialUpdateSkillCommand.SkillId = skillId;

            await mediator.Send(partialUpdateSkillCommand);

            return Ok("Updated Successfully");

        }

        [HttpDelete("{skillId}")]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid employeeId, [FromRoute] int skillId, [FromQuery] bool softDelete)
        {
            var requestDto = new DeleteSkillCommand()
            {
                EmployeeId = employeeId,
                SkillId = skillId,
                SoftDelete = softDelete
            };

            await mediator.Send(requestDto);

            return NoContent();
        }
    }
}
