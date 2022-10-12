using AutoMapper;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.DeleteDegree;
using CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails;
using CVBuilder.Application.Features.Degrees.Queries.GetDegreesList;
using CVBuilder.Application.ViewModels.Degree;
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
    public class DegreesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public DegreesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDegrees([FromRoute] Guid employeeId)
        {
            var requestDto = new GetDegreesListQuery()
            {
                EmployeeId = employeeId,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpGet("{degreeId}")]
        public async Task<IActionResult> GetDegreeDetails([FromRoute] Guid employeeId, [FromRoute] int degreeId)
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
        public async Task<IActionResult> AddDegree([FromRoute] Guid employeeId, [FromBody] DegreeViewModel addDegreeViewModel)
        {

            var requestDto = mapper.Map<AddDegreeCommand>(addDegreeViewModel);

            requestDto.EmployeeId = employeeId;


            var response = await mediator.Send(requestDto);

            return CreatedAtAction(nameof(GetDegreeDetails), new { employeeId = response.EmployeeId, degreeId = response.DegreeId }, response);
        }

        [HttpPut("{degreeId}")]
        public async Task<IActionResult> UpdateDegree([FromRoute] Guid employeeId, [FromRoute] int degreeId, [FromBody]DegreeViewModel degreeViewModel)
        {

            var requestDto = mapper.Map<UpdateDegreeCommand>(degreeViewModel);

            requestDto.EmployeeId = employeeId;
            requestDto.DegreeId = degreeId;

            await mediator.Send(requestDto);

            return Ok(requestDto);
        }

        [HttpPatch("{degreeId}")]
        public async Task<IActionResult> UpdateDegreePartially([FromRoute] Guid employeeId, [FromRoute] int degreeId, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new PartialUpdateDegreeCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.EmployeeId = employeeId;

            requestDto.DegreeId = degreeId;

            await mediator.Send(requestDto);
            
            return Ok("Updated Successfully");
        }


        [HttpDelete("{degreeId}")]
        public async Task<IActionResult> DeleteDegree([FromRoute] Guid employeeId, [FromRoute] int degreeId)
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
