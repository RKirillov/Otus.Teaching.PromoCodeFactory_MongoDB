using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public interface IPreferenceRepository : IRepository<Preference, Guid>
    {
        Task<IEnumerable<Preference>> GetRangeAsync(List<Guid> listIds, CancellationToken cancellationToken);
    }
}