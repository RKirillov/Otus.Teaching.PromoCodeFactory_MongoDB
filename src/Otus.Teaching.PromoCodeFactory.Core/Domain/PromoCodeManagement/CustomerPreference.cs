using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    //Сущность для Many-To-Many Customer/Preference
    public class CustomerPreference
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Guid PreferenceId { get; set; }
        public virtual Preference Preference { get; set; }
    }
}
