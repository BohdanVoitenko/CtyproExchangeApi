using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand;

namespace CryptoExchange.Api.Models
{
	public class LoginDto : IMapWith<LoginUserCommand>
	{
		public string Id { get; set; }
		public string Password { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<LoginDto, LoginUserCommand>()
				.ForMember(loginUserCommand => loginUserCommand.Id, opt =>
				opt.MapFrom(loginDto => loginDto.Id))
				.ForMember(loginUserCommand => loginUserCommand.Password, opt =>
				opt.MapFrom(loginDto => loginDto.Password));
        }
	}
}

