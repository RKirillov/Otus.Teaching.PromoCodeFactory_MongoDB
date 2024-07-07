using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    //сделать обстрактным
    public class MongoDBService: IMongoDBService
    {
        private readonly IMongoCollection<Role> _playlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<Role>(mongoDBSettings.Value.CollectionRoleName);
            database.DropCollection(mongoDBSettings.Value.CollectionRoleName);
        }

        public Task AddToPlaylistAsync(string id, string movieId)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Role playlist)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertManyAsync (List<Role> roles)
        {
            await _playlistCollection.InsertManyAsync(roles);
        }

    }
}
