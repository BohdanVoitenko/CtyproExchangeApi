using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Queries.GetOrderList
{
	public class OrderListDto : IMapWith<Order>
	{
		public Guid Id { get; set; }
		public string Exchanger { get; set; }
		public string ExchangeFrom { get; set; }
		public string ExchangeTo { get; set; }
		public double IncomeSum { get; set; }
		public int OutcomeSum { get; set; } = 1;


		public void Mapping(Profile profile)
        {
			profile.CreateMap<Order, OrderListDto>()
				.ForMember(orderDto => orderDto.Id,
				opt => opt.MapFrom(order => order.Id))
				.ForMember(orderDto => orderDto.Exchanger,
				opt => opt.MapFrom(order => order.Exchanger))
				.ForMember(orderDto => orderDto.ExchangeFrom,
				opt => opt.MapFrom(order => order.ExchangeFrom))
				.ForMember(orderDto => orderDto.ExchangeTo,
				opt => opt.MapFrom(order => order.ExchangeTo))
				.ForMember(orderDto => orderDto.IncomeSum,
				opt => opt.MapFrom(order => order.IncomeSum))
				.ForMember(orderDto => orderDto.OutcomeSum,
				opt => opt.MapFrom(order => order.OutcomeSum));
        }
	}
}

