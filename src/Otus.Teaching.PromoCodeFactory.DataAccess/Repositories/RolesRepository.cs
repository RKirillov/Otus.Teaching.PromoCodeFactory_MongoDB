using System;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями ролей
    public class RolesRepository : Repository<Role, Guid>, IRolesRepository
    {
        public RolesRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
