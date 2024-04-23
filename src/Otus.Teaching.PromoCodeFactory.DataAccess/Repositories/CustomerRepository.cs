using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями клиентов и их предпочтений
    /// </summary>
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {

        }

        #region UpdateAsync
        public async Task<int> UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            //TODO check
            return await _context.Set<Customer>().Where(u => u.Id == entity.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u, entity), cancellationToken);
        }
        #endregion

        #region Update
        public void Update(Customer entity)
        {
            //TODO check
            _context.Set<Customer>().Update(entity);
        }
        #endregion

        #region DeleteAsync
        public async Task<int> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //TODO check
            //            var preferences = _context.Set<Preference>().AsQueryable().Where(t => listIds.Contains(t.Id));
            return await _context.Set<Customer>().Where(u => u.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        #endregion

        #region GetByPreferences
        public async Task<List<Customer>> GetByPreferences(string preferenceName, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Customer>().Where(x => x.Preferences
                .Select(x => x.Preference.Name)
                .Contains(preferenceName)).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetAsync
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Курс. </returns>
        public override async Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //var query = _context.Set<Customer>().AsQueryable();
            //query = query.Where(l => l.Id == id);//&& !l.Deleted
            //var z = MyQuery(() => _context.Set<Customer>().Where(l => l.Id == id));

            var query = _context.Set<Customer>().Where(l => l.Id == id);
            return await query.SingleOrDefaultAsync(cancellationToken);
        }
        #endregion

        protected private Task<List<Customer>> MyQuery (Func<Customer, bool> myFunc)
        {
            return Task.FromResult(new List<Customer>());
        }
    }
}
