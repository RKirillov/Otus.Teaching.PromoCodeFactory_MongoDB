using System;
using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    [AutoMap(typeof(Role), ReverseMap = true)]
    public class RoleItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}