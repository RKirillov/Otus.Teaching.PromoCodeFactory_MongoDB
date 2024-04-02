using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    //Сущность для Many-To-Many Customer/Preference
    public class CustomerPreference
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int PreferenceId { get; set; }
        public Preference Preference { get; set; }
    }
}
