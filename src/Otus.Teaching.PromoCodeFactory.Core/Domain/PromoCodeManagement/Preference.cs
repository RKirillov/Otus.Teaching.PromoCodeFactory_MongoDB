using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference
        :BaseEntity
    {
        public string Name { get; set; }

        public Guid PromoCodeId { get; set; }
        //добавил
        public PromoCode PromoCode { get; set; }

        public ICollection<CustomerPreference> CustomerPreference { get; set; }
    }
}