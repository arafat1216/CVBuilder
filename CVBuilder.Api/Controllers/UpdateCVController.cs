using AutoMapper;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddWorkExperienceRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteWorkExperienceRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateWorkExperienceRequest;
using CVBuilder.Application.ViewModels.UpdateResourceRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UpdateCVController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateCVController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("add-degree")]
        public async Task<IActionResult> AddDegree([FromBody] AddDegreeRequestViewModel viewModel)
        {
            var requestDto = mapper.Map<AddDegreeRequestCommand>(viewModel);

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPatch("update-degree")]
        
        public async Task<IActionResult> UpdateDegree([FromQuery] int degreeId, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new UpdateDegreeRequestCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.DegreeId = degreeId;

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }

        [HttpDelete("delete-degree")]
        public async Task<IActionResult> DeleteDegree([FromQuery] int degreeId, [FromBody] DeleteDegreeRequestCommand requestDto)
        {    
            var response = await mediator.Send(requestDto);

            return Ok(response);
        }

        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequestViewModel viewModel)
        {
            var requestDto = mapper.Map<AddProjectRequestCommand>(viewModel);

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPatch("update-project")]
        public async Task<IActionResult> UpdateProject([FromQuery] int projectId, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new UpdateProjectRequestCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.ProjectId = projectId;

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpDelete("delete-project")]
        public async Task<IActionResult> DeleteProject([FromQuery] int projectId, [FromBody] DeleteProjectRequestCommand requestDto)
        {
            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPost("add-skill")]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillRequestViewModel viewModel)
        {
            var requestDto = mapper.Map<AddSkillRequestCommand>(viewModel);

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPatch("update-skill")]
        public async Task<IActionResult> UpdateSkill([FromQuery] int skillId, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new UpdateSkillRequestCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.SkillId = skillId;

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpDelete("delete-skill")]
        public async Task<IActionResult> DeleteSkill([FromQuery] int skillId, [FromBody] DeleteSkillRequestCommand requestDto)
        {
            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPost("add-work-experience")]
        public async Task<IActionResult> AddWorkExperience([FromBody] AddWorkExperienceRequestViewModel viewModel)
        {
            var requestDto = mapper.Map<AddWorkExperienceRequestCommand>(viewModel);

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPatch("update-work-experience")]
        public async Task<IActionResult> UpdateWorkExperience([FromQuery] int workExperienceId, [FromBody] JsonPatchDocument patchDocument)
        {
            var requestDto = new UpdateWorkExperienceRequestCommand();

            patchDocument.ApplyTo(requestDto);

            requestDto.WorkExperienceId = workExperienceId;

            if (!TryValidateModel(requestDto))
            {
                return ValidationProblem();
            }

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpDelete("delete-work-experience")]
        public async Task<IActionResult> DeleteWorkExperience([FromQuery] int workExperienceId, [FromBody] DeleteWorkExperienceRequestCommand requestDto)
        {
            var response = await mediator.Send(requestDto);

            return Ok(response);
        }
    }

    
}
