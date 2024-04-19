using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями промокодов
    /// </summary>
    public class PromoCodeRepository : Repository<PromoCode, Guid>, IPromoCodeRepository
    {
        public PromoCodeRepository(DatabaseContext context) : base(context)
        {

        }

        #region UpdateAsync
/*        public async Task<int> DeleteByCustomerIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //TODO check
            //            var preferences = _context.Set<Preference>().AsQueryable().Where(t => listIds.Contains(t.Id));
            return await _context.Set<PromoCode>().Where(u => u.CustomerId == id)
                .ExecuteDeleteAsync(cancellationToken);
        }*/
        #endregion
    }
}
