using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.DeleteSkill;
using CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateSkillService : IUpdateResourceService, IUpdateSkillService
    {
        private readonly ISkillUpdateRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateSkillService(ISkillUpdateRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            // get resource details
            var skillUpdateRequest = await GetSkillUpdateRequest(resourceRequest.RequestId);

            if (resourceRequest.RequestType == RequestType.Add.ToString())
            {
                await AddSkill(resourceRequest, skillUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Modify.ToString())
            {
                await UpdateSkill(resourceRequest, skillUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Remove.ToString())
            {
                await DeleteSkill(resourceRequest, skillUpdateRequest);
            }
        }

        private async Task DeleteSkill(ResourceRequest resourceRequest, SkillUpdateRequest skillUpdateRequest)
        {
            var requestDto = mapper.Map<DeleteSkillCommand>(skillUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            requestDto.SoftDelete = true;

            await mediator.Send(requestDto);
        }

        private async Task UpdateSkill(ResourceRequest resourceRequest, SkillUpdateRequest skillUpdateRequest)
        {
            var requestDto = mapper.Map<PartialUpdateSkillCommand>(skillUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task AddSkill(ResourceRequest resourceRequest, SkillUpdateRequest skillUpdateRequest)
        {
            var requestDto = mapper.Map<AddSkillCommand>(skillUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task<SkillUpdateRequest?> GetSkillUpdateRequest(int requestId)
        {
            return await repository.GetSkillUpdateRequestByIdAsync(requestId);
        }
    }
}
