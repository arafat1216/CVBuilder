﻿using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails
{
    public class GetDegreeDetailsQueryHandler : IRequestHandler<GetDegreeDetailsQuery,DegreeViewModel>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public GetDegreeDetailsQueryHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DegreeViewModel> Handle(GetDegreeDetailsQuery request, CancellationToken cancellationToken)
        {
            #region check if degree exists

            var degreeExists = await repository.ExistsAsync(request.EmployeeId, request.DegreeId);

            if (!degreeExists)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            #endregion

            #region fetch degree details

            var degreeDetails = await repository.GetDegreeByIdAsync(request.EmployeeId, request.DegreeId);

            #endregion

            #region mapping degree entity to degree view model

            var degreeDetailsDto = mapper.Map<DegreeViewModel>(degreeDetails);

            #endregion

            return degreeDetailsDto;
        }
    }
}