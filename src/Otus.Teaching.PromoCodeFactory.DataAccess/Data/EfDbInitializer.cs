using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly IMongoRoleRepository _mRoleRepository;
        private readonly IMongoEmployeeRepository _mEmployeeRepository;
        public EfDbInitializer(IMongoRoleRepository mRoleRepository, IMongoEmployeeRepository mEmployeeRepository)
        {

            _mRoleRepository = mRoleRepository;

            _mEmployeeRepository = mEmployeeRepository;
        }
        
        public void InitializeDb()
        {
            _mRoleRepository.InsertManyAsync(FakeDataFactory.Roles);
            _mEmployeeRepository.InsertManyAsync(FakeDataFactory.Employees);
        }
    }
}