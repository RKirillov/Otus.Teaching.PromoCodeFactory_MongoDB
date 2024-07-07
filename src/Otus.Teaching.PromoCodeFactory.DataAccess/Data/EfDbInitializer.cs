using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly IRoleRepository _mRoleRepository;

        public EfDbInitializer(IRoleRepository mongoDBService)
        {

            _mRoleRepository = mongoDBService;
        }
        
        public void InitializeDb()
        {
            _mRoleRepository.InsertManyAsync(FakeDataFactory.Roles);
        }
    }
}