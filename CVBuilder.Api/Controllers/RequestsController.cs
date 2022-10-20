using CVBuilder.Application.Features.ResourceRequests.Queries.ListAllRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RequestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var requestDto = new ListAllResourceRequestsQuery();

            var response = await mediator.Send(requestDto);

            return Ok(response);  
        }

        [HttpPost("{requestId}")]
        public async Task<IActionResult> PostRequest([FromRoute] int requestId, [FromQuery] bool approve)
        {
            return Ok();
        }
    }
}
