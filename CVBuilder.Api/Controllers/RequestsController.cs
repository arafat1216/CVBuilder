using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateRequest;
using CVBuilder.Application.Features.ResourceRequests.Queries.GetAllRequestsList;
using CVBuilder.Application.Features.ResourceRequests.Queries.GetMyRequestsList;
using CVBuilder.Application.Features.ResourceRequests.Queries.GetRequestDetails;
using CVBuilder.Domain.Enums;
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
        [Authorize(Roles = "Admin,Basic")]
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllRequests([FromQuery] Status? status, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            if (pageSize > maximumPageSize)
                pageSize = maximumPageSize;

            var requestDto = new GetAllRequestsListQuery()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                Status = status.ToString(),
            };

            var (requestsList, metaData) = await mediator.Send(requestDto);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(requestsList);
        }

        [HttpGet("{requestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRequestDetails([FromRoute] int requestId)
        {
            var requestDto = new GetRequestDetailsQuery() 
            {
                RequestId = requestId
            };

            var response = await mediator.Send(requestDto);

            return Ok(response);
        }


        [HttpPut("{requestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRequest([FromRoute] int requestId, [FromQuery] bool approved)
        {
            var requestDto = new UpdateRequestCommand()
            {
                RequestId = requestId,
                IsApproved = approved
            };

            await mediator.Send(requestDto);
            
            return NoContent();
        }
    }
}
