using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Exchangers.Queries
{
    public class ExchangerInfoVm : IMapWith<Exchanger>
    {
        public string ExchangerName { get; set; }
        public string WebResourceUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Exchanger, ExchangerInfoVm>()
                .ForMember(vm => vm.ExchangerName,
                opt => opt.MapFrom(exchanger => exchanger.Name))
                .ForMember(vm => vm.WebResourceUrl,
                opt => opt.MapFrom(exchanger => exchanger.WebResuorceUrl));
        }
    }
}

