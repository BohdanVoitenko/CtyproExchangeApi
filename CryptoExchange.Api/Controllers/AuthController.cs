using System;
using System.Security.Claims;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.UsersAuth.Commands;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand;
using CryptoExchange.Application.UsersAuth.Commands.LogoutUserCommand;
using CryptoExchange.Domain;
using CryptoExchange.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : BaseController
	{
		private readonly IMapper _mapper;

		public AuthController(IMapper mapper) => _mapper = mapper;


		[HttpPost("register")]
		public async Task<ActionResult<Guid>> Register([FromBody] CreateUserDto createUserDto)
        {

			var command = _mapper.Map<CreateUserCommand>(createUserDto);

			var userId = await Mediator.Send(command);
			return Ok($"New account was successfully created. Here is your id: {userId}");
        }

		[HttpGet("login")]
		public async Task<ActionResult<string>> Login([FromBody]LoginDto loginDto)
        {
			var command = _mapper.Map<LoginUserCommand>(loginDto);

			var userName = await Mediator.Send(command);
			return Ok($"Hello, {userName}, you have successfully signed in to your account");
        }

		[Authorize]
		[HttpGet("logout")]
		public async Task<ActionResult<string>> Logout([FromBody]LogoutDto logoutDto)
        {
			var command = _mapper.Map<LogoutUserCommand>(logoutDto);

			var userName = await Mediator.Send(command);
			return Ok($"{userName}, you have successfully signed out of your account");
        }
	}
}

