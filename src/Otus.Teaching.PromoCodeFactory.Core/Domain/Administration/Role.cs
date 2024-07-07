using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : BaseEntity
    {
        public string Name { get; set; }

        //добавил
        //public virtual Employee Employee { get; set; }//можно не указывать
        public string Description { get; set; }
    }
}