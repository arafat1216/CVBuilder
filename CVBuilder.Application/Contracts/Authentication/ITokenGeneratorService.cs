using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Contracts.Authentication
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(Employee employee);
        string GenerateToken(Company company);
    }
}
