using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.DeleteDegree;
using CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateDegreeService : IUpdateResourceService, IUpdateDegreeService
    {
        private readonly IDegreeUpdateRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateDegreeService(IDegreeUpdateRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            // get resource details
            var degreeUpdateRequest = await GetDegreeUpdateRequest(resourceRequest.RequestId);

            if (resourceRequest.RequestType == RequestType.Add.ToString())
            {
                await AddDegree(resourceRequest, degreeUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Modify.ToString())
            {
                await UpdateDegree(resourceRequest, degreeUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Remove.ToString())
            {
                await DeleteDegree(resourceRequest, degreeUpdateRequest);
            }
        }

        private async Task DeleteDegree(ResourceRequest resourceRequest, DegreeUpdateRequest degreeUpdateRequest)
        {
            var requestDto = mapper.Map<DeleteDegreeCommand>(degreeUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            requestDto.SoftDelete = true;

            await mediator.Send(requestDto);
        }

        private async Task UpdateDegree(ResourceRequest resourceRequest, DegreeUpdateRequest degreeUpdateRequest)
        {
            var requestDto = mapper.Map<UpdateDegreeCommand>(degreeUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task AddDegree(ResourceRequest resourceRequest, DegreeUpdateRequest degreeUpdateRequest)
        {
            var requestDto = mapper.Map<AddDegreeCommand>(degreeUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task<DegreeUpdateRequest?> GetDegreeUpdateRequest(int requestId)
        {
            return await repository.GetDegreeUpdateRequestByIdAsync(requestId);
        }
    }
}
