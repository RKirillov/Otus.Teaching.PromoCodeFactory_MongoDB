using Microsoft.Extensions.Options;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Options;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB
{
    //сделать обстрактным
    public sealed class RoleRepository : MongoBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IOptions<MongoDBSettings> mongoDBSettings)
            : base(mongoDBSettings, mongoDBSettings.Value.CollectionRoleName)
        {
        }

    }
}
