using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.HandleResourceRequest
{
    public class UpdateResourceRequestCommandHandler : IRequestHandler<UpdateResourceRequestCommand>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;

        public UpdateResourceRequestCommandHandler(IResourceRequestRepository resourceRequestRepository)
        {
            this.resourceRequestRepository = resourceRequestRepository;
        }

        public async Task<Unit> Handle(UpdateResourceRequestCommand request, CancellationToken cancellationToken)
        {

            return Unit.Value;


        }
    }
}
