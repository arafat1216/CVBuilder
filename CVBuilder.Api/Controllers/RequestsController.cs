using CVBuilder.Application.Features.ResourceRequests.Queries.GetMyRequestsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CVBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator mediator;
        const int maximumPageSize = 20;

        public RequestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests([FromQuery] string? status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            if (pageSize > maximumPageSize)
                pageSize = maximumPageSize;

            var requestDto = new GetMyRequestsListQuery()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                Status = status,
            };

            var (requestsList, metaData) = await mediator.Send(requestDto);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(requestsList);
        }

        [HttpGet("all-requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            return Ok();
        }


        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateRequest([FromRoute] int requestId, [FromQuery] bool approve)
        {
            return Ok();
        }
    }
}
