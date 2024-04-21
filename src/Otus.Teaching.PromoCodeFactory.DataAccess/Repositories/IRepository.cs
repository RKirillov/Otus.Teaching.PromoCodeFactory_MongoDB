using Otus.Teaching.PromoCodeFactory.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public interface IRepository<T, TPrimaryKey> where T : BaseEntity
    {
        void Delete(T entity);
        T Get(TPrimaryKey id);
        IQueryable<T> GetAll(bool asNoTracking = false);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        Task<T> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        void Update(T entity);
    }
}