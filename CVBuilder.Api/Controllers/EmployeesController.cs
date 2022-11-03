using AutoMapper;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.DeleteEmployee;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Employees.Queries.GetAllEmployeesCVList;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeesList;
using CVBuilder.Application.ViewModels.Employee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        const int maximumPageSize = 20;

        public EmployeesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {

            if (pageSize > maximumPageSize)
                pageSize = maximumPageSize;

            var requestDto = new GetEmployeesListQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var (employees, metaData) = await mediator.Send(requestDto);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(employees);
        }

        [HttpGet("all-employees-cv")]
        public async Task<IActionResult> GetAllEmployeesCV([FromQuery] string? searchBySkill, [FromQuery] string? searchByDegree, [FromQuery] string? searchByProject, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            if (pageNumber > maximumPageSize)
                pageNumber = maximumPageSize;

            var requestDto = new GetAllEmployeesCVListQuery()
            {
                SearchBySkill = searchBySkill,
                searchByDegree = searchByDegree,
                searchByProject = searchByProject,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var (employees, metaData) = await mediator.Send(requestDto);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(employees);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetails([FromRoute] Guid id)
        {
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = id,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeViewModel addEmployeeViewModel)
        {
            var requestDto = mapper.Map<AddEmployeeCommand>(addEmployeeViewModel);

            var response = await mediator.Send(requestDto);

            return CreatedAtAction(nameof(GetEmployeeDetails),new {id = response.EmployeeId},response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            var requestDto = mapper.Map<UpdateEmployeeCommand>(updateEmployeeViewModel);
            requestDto.EmployeeId = id;

            await mediator.Send(requestDto);

            return Ok(requestDto);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployeePartially([FromRoute] Guid id, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new PartialUpdateEmployeeCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.EmployeeId = id;

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            await mediator.Send(requestDto);

            return Ok("Updated Successfully");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id, [FromQuery] bool softDelete)
        {
            var requestDto = new DeleteEmployeeCommand()
            {
                EmployeeId = id,
                SoftDelete = softDelete
            };

            await mediator.Send(requestDto);

            return NoContent();
        }

    }
}
