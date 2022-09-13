using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.UsersAuth.Commands;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;

namespace CryptoExchange.Api.Models
{
	public class CreateUserDto : IMapWith<CreateUserCommand>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		

		public void Mapping(Profile profile)
        {
			profile.CreateMap<CreateUserDto, CreateUserCommand>()
				.ForMember(createUserCommand => createUserCommand.Email,
					opt => opt.MapFrom(createUserDto => createUserDto.Email))
				.ForMember(createUserCommand => createUserCommand.Password,
					opt => opt.MapFrom(createUserDto => createUserDto.Password))
				.ForMember(createUserCommand => createUserCommand.UserName,
					opt => opt.MapFrom(createUserDto => createUserDto.UserName));
        }
	}
}

