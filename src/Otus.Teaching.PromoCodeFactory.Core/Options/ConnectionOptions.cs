using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Options
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; set; }

        public string MongoDB { get; set; }
    }
}