using System;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Common.Caching;
using CryptoExchange.Application.Exchangers.Commands;
using CryptoExchange.Application.Exchangers.Commands.CreateExchanger;
using CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders;
using CryptoExchange.Application.Exchangers.Queries;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
	[Authorize]
	public class ExchangerController : BaseController
	{
		private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<ExchangerController> _logger;

        public ExchangerController(IMapper mapper, IUserService userService, ILogger<ExchangerController> logger)
            => (_mapper, _userService, _logger) = (mapper, userService, logger);


        /// <summary>
        /// Create new exchanger
        /// </summary>
        /// <remarks>
        ///	Sample request:
        ///	POST exchanger/create
        ///	{
        ///	    "UserId":"2a562f06-934b-4093-b698-2d7093c62e17",
        ///	    "Name":"SomeExchange",
        ///	    "WebResourceUrl":"https://exampleurl.com"
        ///	}
        /// </remarks>
        /// <param name="createExchangerDto">CreateExchangerDto object</param>
        /// <returns>Returns CreateExchangerResultVm Object</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User is unaithorized</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CreateExchangerResultVm>> Create([FromBody]CreateExchangerDto createExchangerDto)
        {
			var command = _mapper.Map<CreateExchangerCommand>(createExchangerDto);

			var result = await Mediator.Send(command);

            if(result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());
                return BadRequest(result);
            }

			return Ok(result);
        }

        /// <summary>
        /// Gets information about exchanger
        /// </summary>
        /// <param name="exchangerName">string with exchanger's name</param>
        /// <remarks>
        /// Sample request:
        /// GET exchanger/info/SomeExchanger
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User is unauthorized</response>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{exchangerName}")]
        [Cached(60)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExchangerInfoVm>> GetInfo(string exchangerName)
        {
			var exchangerInfoDto = new ExchangerInfoDto { Exchanger = exchangerName };

            var query = _mapper.Map<GetExchangerInfoQuery>(exchangerInfoDto);

			var result = await Mediator.Send(query);

			return Ok(result);
        }

        /// <summary>
        /// Delete all exchanger's orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE exchanger/deleteAllOrders
        /// {
        ///     "ExchangerId":"5b1327b7-79d6-429c-9be2-fc81642ab639"
        /// }
        /// </remarks>
        /// <param name="deleteOrdersdto">DeleteAllOrdersForExchangerDto object</param>
        /// <returns>Returns DeleteAllOrdersResultVm Object</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User is unauthorized</response>
        [Authorize]
        [HttpDelete("deleteAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DeleteAllOrdersResultVm>> DeleteAll([FromBody]DeleteAllOrdersForExchangerDto deleteOrdersdto)
        {
			var command = _mapper.Map<DeleteAllOrdersCommand>(deleteOrdersdto);

			var result = await Mediator.Send(command);

            if(result.Error is not null)
            {
                _logger.LogError(result.Error);
                _logger.LogTrace(_userService.GetTraceIdentifier());
                _logger.LogInformation(_userService.GetUserName());
                return BadRequest(result);
            }
			return Ok(result);
        }
	}
}

