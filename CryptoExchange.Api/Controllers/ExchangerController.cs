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
	[Route("api/[controller]")]
	[Authorize]
	public class ExchangerController : BaseController
	{
		private readonly IMapper _mapper;

		public ExchangerController(IMapper mapper) => _mapper = mapper;

		[HttpPost("create")]
		public async Task<ActionResult<Guid>> Create([FromBody]CreateExchangerDto createExchangerDto)
        {
			var command = _mapper.Map<CreateExchangerCommand>(createExchangerDto);

			var exchangerId = await Mediator.Send(command);

			return Ok(exchangerId);
        }

        [HttpGet("exchangerInfo")]
        [Cached(60)]
		public async Task<ActionResult<ExchangerInfoVm>> GetInfo([FromBody] ExchangerInfoDto exchangerInfoDto)
        {
			var query = _mapper.Map<GetExchangerInfoQuery>(exchangerInfoDto);

			var result = await Mediator.Send(query);

			return Ok(result);
        }

        [HttpDelete("deleteAllOrders")]
		public async Task<ActionResult> DeleteAll([FromBody]DeleteAllOrdersForExchangerDto deleteOrdersdto)
        {
			var command = _mapper.Map<DeleteAllOrdersCommand>(deleteOrdersdto);

			await Mediator.Send(command);
			return Ok();
        }
	}
}

