﻿using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
        Task AddAsync(Customer entity);
    }
}