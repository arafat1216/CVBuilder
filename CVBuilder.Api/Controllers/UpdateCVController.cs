using AutoMapper;
using CVBuilder.Application.Contracts.UpdateCVRequest;
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
using CVBuilder.Application.ViewModels.UpdateCV;
using CVBuilder.Application.ViewModels.UpdateResourceRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUpdateCVRequestService updateCVRequestService;

        public UpdateCVController(IMediator mediator, IMapper mapper, IUpdateCVRequestService updateCVRequestService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.updateCVRequestService = updateCVRequestService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCV([FromBody] UpdateCVViewModel updateCVViewModel)
        {
            var responses = await updateCVRequestService.HandleRequest(updateCVViewModel);

            return Ok(responses);
        }



    }

    
}
