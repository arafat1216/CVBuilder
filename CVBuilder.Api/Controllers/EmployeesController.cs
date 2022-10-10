using AutoMapper;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.DeleteEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail;
using CVBuilder.Application.Features.Employees.Queries.GetEmployeesList;
using CVBuilder.Application.ViewModels.Employee;
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
        private readonly IMapper mapper;

        public EmployeesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var dtos = await mediator.Send(new GetEmployeesListQuery());
            return Ok(dtos);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
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
