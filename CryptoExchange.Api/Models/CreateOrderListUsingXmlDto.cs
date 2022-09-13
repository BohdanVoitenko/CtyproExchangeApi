using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Orders.Commands.CreateFromXmlFile;

namespace CryptoExchange.Api.Models
{
	public class CreateOrderListUsingXmlDto : IMapWith<CreateOrderListCommand>
	{
		public Guid ExchangerId { get; set; }
		public string FilePath { get; set; }


		public void Mapping(Profile profile)
        {
			profile.CreateMap<CreateOrderListUsingXmlDto, CreateOrderListCommand>()
				.ForMember(query => query.ExchangerId,
				opt => opt.MapFrom(dto => dto.ExchangerId))
				.ForMember(query => query.FilePath,
				opt => opt.MapFrom(dto => dto.FilePath));
        }
	}
}

