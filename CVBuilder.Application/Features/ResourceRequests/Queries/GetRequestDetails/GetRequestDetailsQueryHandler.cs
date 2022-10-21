using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQueryHandler : IRequestHandler<GetRequestDetailsQuery, ResourceRequestDetailsDto>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IMapper mapper;

        public GetRequestDetailsQueryHandler(IResourceRequestRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResourceRequestDetailsDto> Handle(GetRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var repsonse = await repository.GetResourceRequestDetailsAsync(request.RequestId);

            if (repsonse == null)
                throw new Exceptions.NotFoundException(nameof(ResourceRequest), request.RequestId);

            return mapper.Map<ResourceRequestDetailsDto>(repsonse);
        }
    }
}
