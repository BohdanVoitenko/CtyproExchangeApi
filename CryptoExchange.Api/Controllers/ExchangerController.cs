using System;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Common.Caching;
using CryptoExchange.Application.Exchangers.Commands;
using CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders;
using CryptoExchange.Application.Exchangers.Queries;
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

		public ExchangerController(IMapper mapper) => _mapper = mapper;


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
        /// <returns>Returns OkObjectResult</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User is unaithorized</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateExchangerDto createExchangerDto)
        {
			var command = _mapper.Map<CreateExchangerCommand>(createExchangerDto);

			var exchangerId = await Mediator.Send(command);

			return Ok(exchangerId);
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
        /// <returns>Returns OkResult</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">User is unauthorized</response>
        [Authorize]
        [HttpDelete("deleteAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAll([FromBody]DeleteAllOrdersForExchangerDto deleteOrdersdto)
        {
			var command = _mapper.Map<DeleteAllOrdersCommand>(deleteOrdersdto);

			await Mediator.Send(command);
			return Ok();
        }
	}
}

