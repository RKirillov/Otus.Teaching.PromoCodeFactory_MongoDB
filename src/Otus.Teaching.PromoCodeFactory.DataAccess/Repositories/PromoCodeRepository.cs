using System;
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
