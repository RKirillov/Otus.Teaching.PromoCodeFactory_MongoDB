using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public interface IPromoCodeRepository : IRepository<PromoCode, Guid>
    {
        //Task<int> DeleteByCustomerIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}