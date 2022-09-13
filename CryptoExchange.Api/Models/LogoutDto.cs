using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.UsersAuth.Commands.LogoutUserCommand;

namespace CryptoExchange.Api.Models
{
	public class LogoutDto : IMapWith<LogoutUserCommand>
	{
		public string Id { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<LogoutDto, LogoutUserCommand>()
				.ForMember(logoutUserCommand => logoutUserCommand.Id, opt =>
				opt.MapFrom(logoutDto => logoutDto.Id));
        }
	}
}

