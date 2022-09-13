using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery;

namespace CryptoExchange.Api.Models
{
	public class OrderListByExchangerDto : IMapWith<AllByExchangerQuery>
	{
		public Guid ExchangerId { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<OrderListByExchangerDto, AllByExchangerQuery>()
				.ForMember(query => query.ExchangerId,
				opt => opt.MapFrom(dto => dto.ExchangerId));
        }
	}
}

