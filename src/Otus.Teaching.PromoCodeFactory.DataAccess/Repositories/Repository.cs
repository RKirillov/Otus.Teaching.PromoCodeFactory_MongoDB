
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    /// <typeparam name="T"> Тип сущности. </typeparam>
    /// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
    public abstract class Repository<T, TPrimaryKey> : IRepository<T, TPrimaryKey> 
        where T: BaseEntity
        //: class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext Context;
        private readonly DbSet<T> _entitySet;

        protected Repository(DbContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }
        #region Get

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        public virtual T Get(TPrimaryKey id)
        {
            return _entitySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public virtual async Task<T> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
        {
            //            return await _entitySet.FindAsync(id, cancellationToken);
            return await _entitySet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> IQueryable массив сущностей. </returns>
        /// TODO Зачему тут IQueryable?
        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll(asNoTracking).ToListAsync(cancellationToken);
        }
        #endregion

        #region SaveChanges
        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
