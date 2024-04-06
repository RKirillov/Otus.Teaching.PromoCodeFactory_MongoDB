using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public interface IEmployeeRepository: IRepository<Employee,Guid>
    {
    }
}