using AutoMapper;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    [AutoMap(typeof(Preference), ReverseMap = true)]
    public class PreferenceResponse
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}