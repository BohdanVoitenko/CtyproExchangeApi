using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Exchangers.Commands;

namespace CryptoExchange.Api.Models
{
	public class CreateExchangerDto : IMapWith<CreateExchangerCommand>
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string WebResourceUrl { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<CreateExchangerDto, CreateExchangerCommand>()
			.ForMember(createExchangerCommand => createExchangerCommand.UserId, opt =>
				opt.MapFrom(createExchangerDto => createExchangerDto.UserId))
			.ForMember(createExchangerCommand => createExchangerCommand.Name, opt =>
				opt.MapFrom(createExchangerDto => createExchangerDto.Name))
			.ForMember(createExchangerCommand => createExchangerCommand.WebResourceUrl,
			opt => opt.MapFrom(createExchangerDto => createExchangerDto.WebResourceUrl));
		}
	}
}

