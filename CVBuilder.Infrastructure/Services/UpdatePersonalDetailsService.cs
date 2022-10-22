using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdatePersonalDetailsService : IUpdateResourceService, IUpdatePersonalDetailsService
    {
        private readonly IPersonalDetailsUpdateRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdatePersonalDetailsService(IPersonalDetailsUpdateRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            // get reosurce details
            var personalDetailsUpdateRequest = await GetPersonalDetailsUpdateRequest(resourceRequest.RequestId);

            var requestDto = mapper.Map<PartialUpdateEmployeeCommand>(personalDetailsUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task<PersonalDetailsUpdateRequest?> GetPersonalDetailsUpdateRequest(int requestId)
        {
            return await repository.GetPersonalDetailsUpdateRequestByIdAsync(requestId);
        }
    }
}
