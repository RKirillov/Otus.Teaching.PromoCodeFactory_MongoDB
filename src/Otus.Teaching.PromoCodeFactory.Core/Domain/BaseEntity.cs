using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain
{
    public class BaseEntity
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
    }
}