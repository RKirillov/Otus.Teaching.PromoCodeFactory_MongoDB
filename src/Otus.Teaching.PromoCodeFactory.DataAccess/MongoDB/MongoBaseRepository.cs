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
        public IMongoCollection<Entity> Collection { get; }

        public MongoBaseRepository(IOptions<MongoDBSettings> mongoDBSettings, string collectionName)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            Collection = database.GetCollection<Entity>(collectionName);
            database.DropCollection(collectionName);
        }

        public Task AddToPlaylistAsync(string id, string movieId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Entity playlist)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entity>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertManyAsync(List<Entity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }
    }
}
