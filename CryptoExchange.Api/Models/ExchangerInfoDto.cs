using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Exchangers.Queries;

namespace CryptoExchange.Api.Models
{
    public class ExchangerInfoDto : IMapWith<GetExchangerInfoQuery>
    {
        public string Exchanger { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExchangerInfoDto, GetExchangerInfoQuery>()
                .ForMember(query => query.ExchangerName,
                opt => opt.MapFrom(dto => dto.Exchanger));
        }
    }
}

