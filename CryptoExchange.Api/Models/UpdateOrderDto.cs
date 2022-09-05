using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Orders.Commands.UpdateOrder;

namespace CryptoExchange.Api.Models
{
	public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
	{
		public Guid Id { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public double In { get; set; }
		public int Out { get; set; } = 1;
		public double Amount { get; set; }
		public double MinAmount { get; set; }
		public double MaxAmount { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
				.ForMember(orderCommand => orderCommand.OrderId,
				opt => opt.MapFrom(orderDto => orderDto.Id))
				.ForMember(orderCommand => orderCommand.From,
				opt => opt.MapFrom(orderDto => orderDto.From))
				.ForMember(orderCommand => orderCommand.To,
				opt => opt.MapFrom(orderDto => orderDto.To))
				.ForMember(orderCommand => orderCommand.In,
				opt => opt.MapFrom(orderDto => orderDto.In))
				.ForMember(orderCommand => orderCommand.Out,
				opt => opt.MapFrom(orderDto => orderDto.Out))
				.ForMember(orderCommand => orderCommand.Amount,
				opt => opt.MapFrom(orderDto => orderDto.Amount))
				.ForMember(orderCommand => orderCommand.MinAmount,
				opt => opt.MapFrom(orderDto => orderDto.MinAmount))
				.ForMember(orderCommand => orderCommand.MaxAmount,
				opt => opt.MapFrom(orderDto => orderDto.MaxAmount));
		}
	}
}

