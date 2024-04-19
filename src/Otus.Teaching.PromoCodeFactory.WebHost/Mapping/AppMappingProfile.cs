using AutoMapper;
using FluentAssertions.Execution;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            //CustomerPreference
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.PreferencesResponse, opt => opt.MapFrom(src => src.Preferences
                    .Select(src => src.Preference)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PromoCodesShotResponse, opt => opt.MapFrom(src => src.PromoCodes))
                .ReverseMap()
                ;
            CreateMap<Preference, PreferenceResponse>()
                ;
            CreateMap<GivePromoCodeRequest, PromoCode>()
                .ForMember(dest => dest.ServiceInfo, opt => opt.MapFrom(src => src.ServiceInfo))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PromoCode))
                .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.PartnerName))
                .ForSourceMember(dest => dest.PreferenceName, opt => opt.DoNotValidate())
                .ReverseMap()
                ;
        }
    }
}
