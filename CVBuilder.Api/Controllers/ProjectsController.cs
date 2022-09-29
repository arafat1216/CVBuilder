using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.DeleteProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Application.Features.Projects.Queries.GetProjectDetails;
using CVBuilder.Application.Features.Projects.Queries.GetProjectsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/employees/{employeeId}/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProjectsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects(Guid employeeId)
        {
            var requestDto = new GetProjectsListQuery()
            {
                EmployeeId = employeeId,
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectDetails(Guid employeeId, int projectId)
        {
            var requestDto = new GetProjectDetailsQuery()
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
            };

            var resultDto = await mediator.Send(requestDto);

            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Guid employeeId, AddProjectCommand addProjectCommand)
        {
            if (employeeId != addProjectCommand.EmployeeId)
                return BadRequest();

            var response = await mediator.Send(addProjectCommand);

            return CreatedAtAction(nameof(GetProjectDetails), new { employeeId = response.EmployeeId, projectId = response.ProjectId }, response);
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject(Guid employeeId, int projectId, UpdateProjectCommand updateProjectCommand)
        {
            if((employeeId != updateProjectCommand.EmployeeId) || (projectId != updateProjectCommand.ProjectId))
                return BadRequest();

            await mediator.Send(updateProjectCommand);

            return NoContent();
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(Guid employeeId, int projectId)
        {
            var requestDto = new DeleteProjectCommand()
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
            };

            await mediator.Send(requestDto);

            return NoContent();
        }

    }

    
}
