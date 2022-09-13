using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;

namespace CryptoExchange.Api.Models
{
	public class DeleteOrderDto : IMapWith<DeleteOrderCommand>
	{
		public Guid OrderId { get; set; }
		public Guid ExchangerId { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<DeleteOrderDto, DeleteOrderCommand>()
				.ForMember(deleteCommand => deleteCommand.ExchangerId,
				opt => opt.MapFrom(deleteDto => deleteDto.ExchangerId))
				.ForMember(deleteCommand => deleteCommand.OrderId,
				opt => opt.MapFrom(deleteDto => deleteDto.OrderId));
        }
	}
}

