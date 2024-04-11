using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : BaseEntity
    {
        public string Name { get; set; }

        //добавил
        //public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Description { get; set; }
    }
}