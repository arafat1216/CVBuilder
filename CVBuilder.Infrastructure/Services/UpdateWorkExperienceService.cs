using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateWorkExperienceService : IUpdateResourceService, IUpdateWorkExperienceService
    {
        private readonly IWorkExperienceUpdateRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateWorkExperienceService(IWorkExperienceUpdateRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            // get resource details 
            var workExperienceUpdateRequest = await GetWorkExperienceUpdateRequest(resourceRequest.RequestId);

            if (resourceRequest.RequestType == RequestType.Add.ToString())
            {
                await AddWorkExperience(resourceRequest, workExperienceUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Modify.ToString())
            {
                await UpdateWorkExperience(resourceRequest, workExperienceUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Remove.ToString())
            {
                await DeleteWorkExperience(resourceRequest, workExperienceUpdateRequest);
            }
        }

        private async Task DeleteWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest workExperienceUpdateRequest)
        {
            var requestDto = mapper.Map<DeleteWorkExperienceCommand>(workExperienceUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            requestDto.SoftDelete = true;

            await mediator.Send(requestDto);
        }

        private async Task UpdateWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest workExperienceUpdateRequest)
        {
            var requestDto = mapper.Map<PartialUpdateWorkExperienceCommand>(workExperienceUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task AddWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest workExperienceUpdateRequest)
        {
            var requestDto = mapper.Map<AddWorkExperienceCommand>(workExperienceUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task<WorkExperienceUpdateRequest?> GetWorkExperienceUpdateRequest(int requestId)
        {
            return await repository.GetWorkExperienceUpdateRequestByIdAsync(requestId);
        }
    }
}
