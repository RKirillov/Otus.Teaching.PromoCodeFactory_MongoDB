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
    }
}
