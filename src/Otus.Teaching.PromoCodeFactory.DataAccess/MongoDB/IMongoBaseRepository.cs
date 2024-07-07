using MongoDB.Driver;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB
{
    public interface IMongoBaseRepository<Entity> where Entity : BaseEntity
    {
        // Expose the MongoDB collection via the interface
        IMongoCollection<Entity> Collection { get; }
        Task<List<Entity>> GetAsync();
        Task CreateAsync(Entity playlist);
        Task AddToPlaylistAsync(string id, string movieId);
        Task DeleteAsync(string id);

        Task InsertManyAsync(List<Entity> entities);
    }
}
