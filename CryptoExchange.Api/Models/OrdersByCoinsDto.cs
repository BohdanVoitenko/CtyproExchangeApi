using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Orders.Queries.GetOrderListByCoins;

namespace CryptoExchange.Api.Models
{
	public class OrdersByCoinsDto : IMapWith<GetOrderListByCoinsQuery>
	{
		public string From { get; set; }
		public string To { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<OrdersByCoinsDto, GetOrderListByCoinsQuery>()
				.ForMember(listQuery => listQuery.From,
				opt => opt.MapFrom(listDto => listDto.From))
				.ForMember(listQuery => listQuery.To,
				opt => opt.MapFrom(listDto => listDto.To));
        }
	}
}

