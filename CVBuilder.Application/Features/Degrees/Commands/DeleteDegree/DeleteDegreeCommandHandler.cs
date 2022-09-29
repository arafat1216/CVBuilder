﻿using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.DeleteDegree
{
    public class DeleteDegreeCommandHandler : IRequestHandler<DeleteDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public DeleteDegreeCommandHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteDegreeCommand request, CancellationToken cancellationToken)
        {
            #region fetch degree  

            var degreeToDelete = await repository.GetDegreeByIdAsync(request.EmployeeId, request.DegreeId);

            #endregion

            if (degreeToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            await repository.DeleteAsync(degreeToDelete);

            return Unit.Value;
        }
    }
}
