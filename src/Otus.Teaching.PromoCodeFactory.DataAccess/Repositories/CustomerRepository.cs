using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями сотрудников и их ролей
    /// </summary>
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(DatabaseContext context) : base(context)
        {

        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Курс. </returns>
        /// TODO Deleted добавлять не стану
        public override async Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = _context.Set<Customer>().AsQueryable();
            query = query
                .Where(l => l.Id == id );//&& !l.Deleted

            return await query.SingleOrDefaultAsync();
            //return await query.SingleOrDefaultAsync(cancellationToken);
        }
    }
}
