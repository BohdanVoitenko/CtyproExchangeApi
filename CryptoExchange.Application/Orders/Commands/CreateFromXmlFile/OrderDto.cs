using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Commands.CreateFromXmlFile
{
	public class OrderDto : IMapWith<Order>
	{
		//public Guid ExchangerId { get; set; }
		public string ExchangerName { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public double In { get; set; } = 1;
		public double Out { get; set; }
		public double Amount { get; set; }
		public double MinAmount { get; set; }
		public double MaxAmount { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<Order, OrderDto>()
				.ForMember(dto => dto.ExchangerName,
				opt => opt.MapFrom(order => order.ExchangerName))
				.ForMember(dto => dto.From,
				opt => opt.MapFrom(order => order.ExchangeFrom))
				.ForMember(dto => dto.To,
				opt => opt.MapFrom(order => order.ExchangeTo))
				.ForMember(dto => dto.In,
				opt => opt.MapFrom(order => order.IncomeSum))
				.ForMember(dto => dto.Out,
				opt => opt.MapFrom(order => order.OutcomeSum))
				.ForMember(dto => dto.Amount,
				opt => opt.MapFrom(order => order.Amount))
				.ForMember(dto => dto.MinAmount,
				opt => opt.MapFrom(order => order.MinAmount))
				.ForMember(dto => dto.MaxAmount,
				opt => opt.MapFrom(order => order.MaxAmount));

		}
	}
}

