using Microsoft.Extensions.Options;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Options;
using Otus.Teaching.PromoCodeFactory.DataAccess.Repositories;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB
{
    //сделать обстрактным
    public sealed class EmployeesRepository : MongoBaseRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(IOptions<MongoDBSettings> mongoDBSettings)
            : base(mongoDBSettings, mongoDBSettings.Value.CollectionEmployeeName)
        {
        }

    }
}
