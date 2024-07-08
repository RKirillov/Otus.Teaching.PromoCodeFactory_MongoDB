using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB
{
    //https://metanit.com/sharp/tutorial/3.38.php
    public abstract class MongoBaseRepository<Entity> : IMongoBaseRepository<Entity> where Entity : BaseEntity
    {
        // Public property to expose the MongoDB collection
        private IMongoCollection<Entity> _collection { get; }

        public MongoBaseRepository(IOptions<MongoDBSettings> mongoDBSettings, string collectionName)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _collection = database.GetCollection<Entity>(collectionName);
            database.DropCollection(collectionName);
        }

        public async Task<Entity> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertOneAsync(Entity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Entity>> GetAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task InsertManyAsync(List<Entity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }
    }
}
