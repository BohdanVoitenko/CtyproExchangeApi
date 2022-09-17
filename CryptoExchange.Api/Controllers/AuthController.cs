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
    [Produces("application/json")]
    [Route("api/[controller]")]
	public class AuthController : BaseController
	{
		private readonly IMapper _mapper;

		public AuthController(IMapper mapper) => _mapper = mapper;

		/// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        ///	Sample request:
        ///	POST /order/register
        ///	{
        ///		"UserName":"Test User",
        ///		"Email":"test@gmail.com",
        ///		"Password":"myStrongPwd."
        /// </remarks>
        /// <param name="createUserDto">CreateUserDto object</param>
        /// <returns>Returns user id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Guid>> Register([FromBody] CreateUserDto createUserDto)
        {

			var command = _mapper.Map<CreateUserCommand>(createUserDto);

			var userId = await Mediator.Send(command);
			return Ok($"New account was successfully created. Here is your id: {userId}");
        }

        /// <summary>
        /// Authorize user
        /// </summary>
        /// <remarks>
        ///	Sample request:
        ///	POST auth/login
        ///	{
        ///		"Id":"5b1327b7-79d6-429c-9be2-fc81642ab639",
        ///		"Password":"myStrongPwd."
        ///	}
        /// </remarks>
        /// <param name="loginDto">LoginDto object</param>
        /// <returns>Returns user id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request/validation failed</response>
        /// <response code="500">User cannot be found</response>
		[HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Login([FromBody]LoginDto loginDto)
        {
			var command = _mapper.Map<LoginUserCommand>(loginDto);

			var userName = await Mediator.Send(command);
			return Ok($"Hello, {userName}, you have successfully signed in to your account");
        }

        /// <summary>
        /// Unauthorize user
        /// </summary>
        /// <remarks>
        ///	Sample request:
        ///	POST auth/logout
        ///	{
        ///		"Id":"5b1327b7-79d6-429c-9be2-fc81642ab639"
        ///	}
        /// </remarks>
        /// <param name="logoutDto">LogoutDto object</param>
        /// <returns>Returns user id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request/validation failed</response>
        /// <response code="500">User cannot be found</response>
        /// <response code="404">User is unauthorized</response>
        [Authorize]
		[HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Logout([FromBody]LogoutDto logoutDto)
        {
			var command = _mapper.Map<LogoutUserCommand>(logoutDto);

			var userName = await Mediator.Send(command);
			return Ok($"{userName}, you have successfully signed out of your account");
        }
	}
}

