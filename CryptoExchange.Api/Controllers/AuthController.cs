using System;
using System.Security.Claims;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Common.JwtAuthentication;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.UsersAuth.Commands;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand;
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
        /// <returns>Returns AuthResult odject</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request/validation failed</response>
		[HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<AuthResult>> Register([FromBody] CreateUserDto createUserDto)
        {

			var command = _mapper.Map<CreateUserCommand>(createUserDto);

			var result = await Mediator.Send(command);

            if (!result.Success) return BadRequest(result);

			return Ok(result);
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
        /// <response code="404">User cannot be found</response>
		[HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthResult>> Login([FromBody]LoginDto loginDto)
        {
			var command = _mapper.Map<LoginUserCommand>(loginDto);

			var result = await Mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

	}
}

