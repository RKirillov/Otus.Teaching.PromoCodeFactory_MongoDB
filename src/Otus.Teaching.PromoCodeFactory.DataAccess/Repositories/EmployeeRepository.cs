using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий дя работы с сущностями сотрудников и их ролей
    /// </summary>
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(DatabaseContext context) : base(context)
        {

        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Курс. </returns>
        /// TODO Deleted добавлять не стану
        public override async Task<Employee> GetAsync(Guid id, CancellationToken cancellationToken)
        {
/*            var query = _context.Set<Employee>().AsQueryable();
            query = query
                .Where(l => l.Id == id );//&& !l.Deleted*/
            var entity = await _context.Set<Employee>().Include(x=>x.Role).FirstOrDefaultAsync(x => x.Id == id);
            return  entity;
            //return await query.SingleOrDefaultAsync(cancellationToken);
        }
    }
}
