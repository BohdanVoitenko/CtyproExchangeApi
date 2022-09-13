using System;
using AutoMapper;
using CryptoExchange.Api.Models;
using CryptoExchange.Application.Orders.Commands.CreateFromXmlFile;
using CryptoExchange.Application.Orders.Commands.CreateOrder;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;
using CryptoExchange.Application.Orders.Commands.UpdateOrder;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersQuery;
using CryptoExchange.Application.Orders.Queries.GetOrderDetails;
using CryptoExchange.Application.Orders.Queries.GetOrderList;
using CryptoExchange.Application.Orders.Queries.GetOrderListByCoins;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api.Controllers
{
	[Route("api/[controller]")]
	public class OrderController : BaseController
	{
		private readonly IMapper _mapper;

		public OrderController(IMapper mapper) => _mapper = mapper;


		[HttpGet("all")]
		public async Task<ActionResult<AllOrdersVm>> GetAll()
        {
			var query = new GetAllOrdersQuery();

			var vm = await Mediator.Send(query);
			return Ok(vm);
        }

		[HttpGet("allbyexchanger")]
		public async Task<ActionResult<AllByExchangerVm>> GetAll([FromBody]OrderListByExchangerDto orderListByExchangerDto)
        {
			var query = _mapper.Map<AllByExchangerQuery>(orderListByExchangerDto);

			var vm = await Mediator.Send(query);
			return Ok(vm);
        }

		[HttpPost("create")]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderDto createOrderDto)
        {
			var command = _mapper.Map<CreateOrderCommand>(createOrderDto);
			//command.UserId = UserId;
			var orderId = await Mediator.Send(command);

			return Ok(orderId);
        }

		[HttpGet("bycoins")]
		public async Task<ActionResult<OrderListByCoinsVm>> OrdersByCoins([FromBody] OrdersByCoinsDto ordersByCoinsDto)
        {
			var query = _mapper.Map<GetOrderListByCoinsQuery>(ordersByCoinsDto);

			var vm = await Mediator.Send(query);
			return Ok(vm);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] UpdateOrderDto updateOrderDto)
        {
            var command = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromBody]DeleteOrderDto deleteOrderDto)
        {
			var command = _mapper.Map<DeleteOrderCommand>(deleteOrderDto);

            await Mediator.Send(command);
            return NoContent();
        }

		[HttpPost("createwithxml")]
		public async Task<ActionResult<OrderListFromXmlVm>> Create([FromBody]CreateOrderListUsingXmlDto createOrderListUsingXmlDto)
        {
			var query = _mapper.Map<CreateOrderListCommand>(createOrderListUsingXmlDto);

			var result = await Mediator.Send(query);
			return Ok(result);
        }
    }
}

