using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System.Collections.Generic;
using System.Collections;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями предпочтений клиентов
    /// </summary>
    public class PreferenceRepository : Repository<Preference, Guid>, IPreferenceRepository
    {
        public PreferenceRepository(DatabaseContext context) : base(context)
        {

        }

        /// <summary>
        /// Получить множество сущностей по Id.
        /// </summary>
        /// <param name="listIds"> лист Id сущности. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Коллекция предпочтений. </returns>
        public async Task<IEnumerable<Preference>> GetRangeAsync(List<Guid> listIds, CancellationToken cancellationToken)
        {
            //TODO check difference
            var preferences = _context.Set<Preference>().AsQueryable().Where(t => listIds.Contains(t.Id));
            //var preferences_ = _context.Set<Preference>().Where(t => listIds.Contains(t.Id));
            return await preferences.ToListAsync(cancellationToken);
        }
    }
}
