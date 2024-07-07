using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Options
{
    public class ConnectionOptions
    {
        public string ConnectionString { get; set; }

        public string MongoDB { get; set; }
    }

    public class MongoDBSettings
    {

        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionRoleName { get; set; } = null!;
        public string CollectionEmployeName { get; set; } = null!;
    }
}