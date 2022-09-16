using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders;

namespace CryptoExchange.Api.Models
{
    public class DeleteAllOrdersForExchangerDto : IMapWith<DeleteAllOrdersCommand>
    {
        public Guid ExchangerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteAllOrdersForExchangerDto, DeleteAllOrdersCommand>()
                .ForMember(command => command.ExchangerId,
                opt => opt.MapFrom(dto => dto.ExchangerId));
        }
    }
}

