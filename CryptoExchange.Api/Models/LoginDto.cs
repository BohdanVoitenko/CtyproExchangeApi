using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand;

namespace CryptoExchange.Api.Models
{
	public class LoginDto : IMapWith<LoginUserCommand>
	{
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

		public void Mapping(Profile profile)
        {
			profile.CreateMap<LoginDto, LoginUserCommand>()
				.ForMember(loginUserCommand => loginUserCommand.UserName, opt =>
				opt.MapFrom(loginDto => loginDto.UserName))
				.ForMember(loginUserCommand => loginUserCommand.Password, opt =>
				opt.MapFrom(loginDto => loginDto.Password))
				.ForMember(loginUserCommand => loginUserCommand.Email, opt =>
				opt.MapFrom(loginDto => loginDto.Email));
        }
	}
}

