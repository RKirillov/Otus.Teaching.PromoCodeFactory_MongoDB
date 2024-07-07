using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public class EfDbInitializer
        : IDbInitializer
    {
        private readonly IMongoDBService _mongoDBService;

        public EfDbInitializer(IMongoDBService mongoDBService)
        {

            _mongoDBService = mongoDBService;
            //database.DropCollection(nameof(Role));
        }
        
        public void InitializeDb()
        {
            _mongoDBService.InsertManyAsync(FakeDataFactory.Roles);

            //_dataContext.AddRange(FakeDataFactory.Employees);
            //_dataContext.SaveChanges();

/*            _dataContext.AddRange(FakeDataFactory.Preferences);
            _dataContext.SaveChanges();

            _dataContext.AddRange(FakeDataFactory.Customers);//необходимо указывать CustomerPreference в отличии от HasData
            _dataContext.SaveChanges();*/

        }
    }
}