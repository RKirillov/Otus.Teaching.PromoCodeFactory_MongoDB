using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly DatabaseContext _dataContext;

        public EfDbInitializer(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void InitializeDb()
        {
            //_dataContext.Database.EnsureDeleted();
            //_dataContext.Database.EnsureCreated();

            //_dataContext.AddRange(FakeDataFactory.Employees);
            //_dataContext.SaveChanges();

/*            _dataContext.AddRange(FakeDataFactory.Preferences);
            _dataContext.SaveChanges();

            _dataContext.AddRange(FakeDataFactory.Customers);//необходимо указывать CustomerPreference в отличии от HasData
            _dataContext.SaveChanges();*/

        }
    }
}