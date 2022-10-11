using AutoMapper;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.DeleteProject;
using CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Application.Features.Projects.Queries.GetProjectDetails;
using CVBuilder.Application.Features.Projects.Queries.GetProjectsList;
using CVBuilder.Application.ViewModels.Project;
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
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ProjectsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromRoute] Guid employeeId)
        {
            var requestDto = new GetProjectsListQuery()
            {
                EmployeeId = employeeId,
            };

            var responseDtos = await mediator.Send(requestDto);

            return Ok(responseDtos);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectDetails([FromRoute] Guid employeeId, [FromRoute] int projectId)
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
        public async Task<IActionResult> AddProject([FromRoute] Guid employeeId, [FromBody] ProjectViewModel projectViewModel)
        {
            var requestDto = mapper.Map<AddProjectCommand>(projectViewModel);

            requestDto.EmployeeId = employeeId;

            var response = await mediator.Send(requestDto);

            return CreatedAtAction(nameof(GetProjectDetails), new { employeeId = response.EmployeeId, projectId = response.ProjectId }, response);
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid employeeId, [FromRoute] int projectId, [FromBody] ProjectViewModel projectViewModel)
        {
            var requestDto = mapper.Map<UpdateProjectCommand>(projectViewModel);

            requestDto.EmployeeId = employeeId;
            requestDto.ProjectId = projectId;

            await mediator.Send(requestDto);

            return Ok(requestDto);
        }

        [HttpPatch("{projectId}")]
        public async Task<IActionResult> UpdateProjectPartially([FromRoute] Guid employeeId, [FromRoute] int projectId, [FromBody] JsonPatchDocument patchDocument)
        {
            var paritalUpdateProjectCommand = new PartialUpdateProjectCommand();

            patchDocument.ApplyTo(paritalUpdateProjectCommand);

            paritalUpdateProjectCommand.EmployeeId = employeeId;

            paritalUpdateProjectCommand.ProjectId = projectId;

            await mediator.Send(paritalUpdateProjectCommand);

            return NoContent();
        }


        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid employeeId, [FromRoute] int projectId)
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
