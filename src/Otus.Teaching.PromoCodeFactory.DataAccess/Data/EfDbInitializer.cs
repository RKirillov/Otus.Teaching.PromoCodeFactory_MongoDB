using Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly IRolesRepository _mRoleRepository;
        private readonly IEmployeesRepository _mEmployeeRepository;
        public EfDbInitializer(IRolesRepository mRoleRepository, IEmployeesRepository mEmployeeRepository)
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