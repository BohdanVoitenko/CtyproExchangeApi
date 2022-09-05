using System;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Orders.Commands.CreateOrder;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;
using CryptoExchange.Application.Orders.Commands.UpdateOrder;
using CryptoExchange.Application.Orders.Queries.GetOrderDetails;
using CryptoExchange.Application.Orders.Queries.GetOrderList;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api.Controllers
{
	[Route("api/[controller]")]
	public class OrderController : BaseController
	{
		private readonly IMapper _mapper;

		public OrderController(IMapper mapper) => _mapper = mapper;

		[HttpGet]
		public async Task<ActionResult<OrderListVm>> GetAll()
		{
			var query = new GetOrderListQuery
			{
				UserId = UserId
			};

			var vm = Mediator.Send(query);
			return Ok(200);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDetailsVm>> Get(Guid id)
		{
			var query = new GetOrderDetailsQuery
			{
				UserId = UserId,
				Id = id
			};

			var vm = Mediator.Send(query);
			return Ok(200);
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderDto createOrderDto)
        {
			var command = _mapper.Map<CreateOrderCommand>(createOrderDto);
			command.UserId = UserId;
			var orderId = await Mediator.Send(command);

			return Ok(orderId);
        }

		[HttpPut]
		public async Task<ActionResult> Update([FromBody] UpdateOrderDto updateOrderDto)
        {
			var command = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
			command.UserId = UserId;
			await Mediator.Send(command);

			return NoContent();
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(Guid id)
        {
			var command = new DeleteOrderCommand
			{
				UserId = UserId,
				Id = id
			};

			await Mediator.Send(command);
			return NoContent();
        }
	}
}

