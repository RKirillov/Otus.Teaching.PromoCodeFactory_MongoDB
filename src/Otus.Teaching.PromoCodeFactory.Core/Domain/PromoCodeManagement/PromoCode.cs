using System;
using System.Runtime;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class PromoCode
        : BaseEntity
    {
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PartnerName { get; set; }

        public Guid PartnerManagerId { get; set; }//если удалить EF добавит сам, но без On Delete Cascade

        public virtual Employee PartnerManager { get; set; }

        public Guid CustomerId { get; set; } //можно удалить - EF все сделает сам, проверить потом.

        public virtual Customer Customer { get; set; }

        public virtual Guid PreferenceId { get; set; }

        public virtual Preference Preference { get; set; }
        //TODO добавлен CustomerId Связь Customer и Promocode реализовать через One-To-Many
    }
}