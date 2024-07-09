using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.MongoDB
{
    public interface IRolesRepository : IMongoBaseRepository<Role>
    {
        /*        Task<List<Role>> GetAsync();
                Task CreateAsync(Role playlist);
                Task AddToPlaylistAsync(string id, string movieId);
                Task DeleteAsync(string id) ;

                Task InsertManyAsync(List<Role> roles);*/
    }
}
