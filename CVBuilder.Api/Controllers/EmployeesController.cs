using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.DeleteEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeesList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var dtos = await mediator.Send(new GetEmployeesListQuery());
            return Ok(dtos);
        }


        [HttpGet("{id}",Name ="GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetails(Guid id)
        {
            var requestDto = new GetEmployeeDetailQuery()
            {
                Id = id,
            };

            var result = await mediator.Send(requestDto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeCommand addEmployeeCommand)
        {
            var response = await mediator.Send(addEmployeeCommand);

            return CreatedAtAction(nameof(GetEmployeeDetails),new {id = response.EmployeeId},response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeCommand updateEmployeeCommand)
        {
            // checks for bad request 
            if(id != updateEmployeeCommand.EmployeeId)
            {
                return BadRequest();
            }

            await mediator.Send(updateEmployeeCommand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var requestDto = new DeleteEmployeeCommand()
            {
                EmployeeId = id,
            };

            await mediator.Send(requestDto);

            return NoContent();
        }

    }
}
