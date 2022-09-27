﻿using CVBuilder.Application.ViewModels;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IRequest<List<EmployeeListViewModel>>
    {
    }
}
