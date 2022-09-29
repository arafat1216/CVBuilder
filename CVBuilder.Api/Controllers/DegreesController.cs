using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.DeleteDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreesList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DegreesController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDegrees(Guid employeeId)
        {
            var requestDto = new GetDegreesListQuery()
            {
                EmployeeId = employeeId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpGet("{degreeId}")]
        public async Task<IActionResult> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            var requestDto = new GetDegreeDetailsQuery()
            {
                DegreeId = degreeId,
                EmployeeId = employeeId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDegree(Guid employeeId, AddDegreeCommand addDegreeCommand)
        {
            if (employeeId != addDegreeCommand.EmployeeId)
                return BadRequest();

            var response = await mediator.Send(addDegreeCommand);

            return CreatedAtAction(nameof(GetDegreeDetails), new { employeeId = response.EmployeeId, degreeId = response.DegreeId }, response);
        }

        [HttpPut("{degreeId}")]
        public async Task<IActionResult> UpdateDegree(Guid employeeId, int degreeId, UpdateDegreeCommand updateDegreeCommand)
        {
            if ((employeeId != updateDegreeCommand.EmployeeId) || (degreeId != updateDegreeCommand.DegreeId))
                return BadRequest();

            await mediator.Send(updateDegreeCommand);

            return NoContent();
        }

        [HttpDelete("{degreeId}")]
        public async Task<IActionResult> DeleteDegree(Guid employeeId, int degreeId)
        {
            var requestDto = new DeleteDegreeCommand() 
            { 
                DegreeId = degreeId,
                EmployeeId = employeeId,
            };

            await mediator.Send(requestDto);

            return NoContent();
        }

    }
}
